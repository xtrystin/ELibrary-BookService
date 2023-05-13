using ELibrary_BookService.Application.Command;
using ELibrary_BookService.Application.Query;
using ELibrary_BookService.Domain.Dapper;

namespace ELibrary_BookService.Application
{
    public static class ProviderCollectionExtension
    {
        public static IServiceCollection AddProviderCollection(this IServiceCollection services)
        {
            // Read Providers
            services.AddScoped<IDapperDataAccess, DapperDataAccess>();
            services.AddScoped<IBookReadProvider, BookReadProvider>();
            
            // Write Providers
            services.AddScoped<IBookProvider, BookProvider>();
            services.AddScoped<ICommonProvider, CommonProvider>();

            return services;
        }
    }
}
