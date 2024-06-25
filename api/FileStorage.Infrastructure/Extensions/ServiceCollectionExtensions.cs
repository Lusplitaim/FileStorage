using FileStorage.Core.Data;
using FileStorage.Core.Data.Storages;
using FileStorage.Infrastructure.Data;
using FileStorage.Infrastructure.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using MongoDBFileStorage = FileStorage.Infrastructure.Data.Storages.FileStorage;

namespace FileStorage.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddUnitOfWork();
            services.AddDatabaseContext(configuration);
            services.AddStorages();

            return services;
        }

        private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            return services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static IServiceCollection AddStorages(this IServiceCollection services)
        {
            return services.AddSingleton<IFileStorage, MongoDBFileStorage>();
        }

        private static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDbContext<DatabaseContext>(opts =>
            {
                opts.UseSqlServer(configuration["DatabaseConnections:SqlServer"], builder => builder.MigrationsAssembly("FileStorage.Infrastructure.Migrations"));
            });
        }
    }
}
