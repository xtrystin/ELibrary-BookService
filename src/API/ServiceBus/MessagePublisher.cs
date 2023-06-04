using MassTransit;
using ServiceBusMessages;

namespace ELibrary_BookService.ServiceBus;

public class MessagePublisher : IMessagePublisher
{
    private readonly IBus _bus;
    private readonly IConfiguration _configuration;

    public MessagePublisher(IBus bus, IConfiguration configuration)
	{
        _bus = bus;
        _configuration = configuration;
    }

    public async Task Publish<T>(T message)
    {
        if (_configuration["Flags:UserRabbitMq"] == "1")
        {
            await _bus.Publish(message);
        }
        else
        {
            // Publisg to many queues -> because Basic Tier ASB allowed only 1-1 queues, no topics
            if (message is BookCreated)
            {
                var m = message as BookCreated;
                var userServiceMessage = new BookCreatedU() { BookId = m.BookId, Amount = m.Amount };
                var borrowingServiceMessage = new BookCreatedBr() { BookId = m.BookId, Amount = m.Amount };
                
                await _bus.Send(userServiceMessage);
                await _bus.Send(borrowingServiceMessage);
            }
            else if (message is BookRemoved)
            {
                var m = message as BookRemoved;
                var userServiceMessage = new BookRemovedU() { BookId = m.BookId };
                var borrowingServiceMessage = new BookRemovedBr() { BookId = m.BookId };

                await _bus.Send(userServiceMessage);
                await _bus.Send(borrowingServiceMessage);
            }
            else if (message is BookAvailabilityChanged)
            {
                var m = message as BookAvailabilityChanged;
                var borrowingServiceMessage = new BookAvailabilityChangedBr() { BookId = m.BookId, Amount = m.Amount };

                await _bus.Send(borrowingServiceMessage);
            }
            else
            {
                // send to one queue
                await _bus.Send(message);
            }
        }
    }
}
