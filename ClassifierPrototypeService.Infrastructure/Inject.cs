
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prototype.ClassifierPrototypeService.Infrastructure.DBContext;

namespace Prototype.ClassifierPrototypeService.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) 
        => services
                .AddRepositories(configuration);

    private static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextFactory<ApplicationDbContext>(o =>
        {
            ServiceOptions serviceOptions = configuration.Get<ServiceOptions>();

            string connectionString = serviceOptions.DB.ConnectionString;
            connectionString = connectionString.TrimEnd(';');

            _ = o.UseNpgsql(connectionString)
                .UseSchema(serviceOptions.DB.Schema)
                .EnableSensitiveDataLogging();
        });
            
        // return services.AddScoped<IFileStorageRepository, FileStorageRepository>();
        return services;
    }
    
}
