using ELibrary_BookService.Consumers;
using ELibrary_BookService.ServiceBus;
using MassTransit;
using ServiceBusMessages;

namespace ELibrary_BookService.RabbitMq
{
    public static class MassTransitCollection
    {
        public static IServiceCollection AddServiceBus(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMessagePublisher, MessagePublisher>();

            services.AddMassTransit(x =>
            {
                // add consumers
                x.AddConsumer<BookAvailabilityChangedConsumer>();
                x.AddConsumer<BookAvailabilityChangedBkConsumer>();


                if (configuration["Flags:UserRabbitMq"] == "1")   //todo change to preprocessor directive #if
                {
                    RabbitMqOptions rabbitMqOptions = configuration.GetSection(nameof(RabbitMqOptions)).Get<RabbitMqOptions>();
                    x.UsingRabbitMq((hostContext, cfg) =>
                    {
                        cfg.Host(rabbitMqOptions.Uri, "/", c =>
                        {
                            c.Username(rabbitMqOptions.UserName);
                            c.Password(rabbitMqOptions.Password);
                        });

                        // Consumers Configuration
                        cfg.ConfigureEndpoints(hostContext);
                    });
                }
                else
                {
                    // Azure Basic Tier - only 1-1 queues
                    x.UsingAzureServiceBus((context, cfg) =>
                    {
                        cfg.Host(configuration["AzureServiceBusConnectionString"]);

                        /// Publisher configuration ///
                        EndpointConvention.Map<BookCreatedBr>(new Uri($"queue:{nameof(BookCreatedBr)}"));
                        cfg.Message<BookCreatedBr>(cfgTopology => cfgTopology.SetEntityName(nameof(BookCreatedBr)));
                        EndpointConvention.Map<BookCreatedU>(new Uri($"queue:{nameof(BookCreatedU)}"));
                        cfg.Message<BookCreatedU>(cfgTopology => cfgTopology.SetEntityName(nameof(BookCreatedU)));

                        EndpointConvention.Map<BookRemovedBr>(new Uri($"queue:{nameof(BookRemovedBr)}"));
                        cfg.Message<BookRemovedBr>(cfgTopology => cfgTopology.SetEntityName(nameof(BookRemovedBr)));
                        EndpointConvention.Map<BookRemovedU>(new Uri($"queue:{nameof(BookRemovedU)}"));
                        cfg.Message<BookRemovedU>(cfgTopology => cfgTopology.SetEntityName(nameof(BookRemovedU)));


                        /// Consumers configuration ///
                        cfg.ReceiveEndpoint("bookavailabilitychangedbk", e =>
                        {
                            e.ConfigureConsumeTopology = false;     // configuration for ASB Basic Tier - queues only
                            e.PublishFaults = false;
                            e.ConfigureConsumer<BookAvailabilityChangedBkConsumer>(context);
                        });
                    });
                }

            });

            return services;
        }
    }
}
