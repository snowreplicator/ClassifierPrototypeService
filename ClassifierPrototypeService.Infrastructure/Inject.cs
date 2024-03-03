using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prototype.ClassifierPrototypeService.Application.Interfaces.QuerySources;
using Prototype.ClassifierPrototypeService.Infrastructure.DBContext;
using Prototype.ClassifierPrototypeService.Infrastructure.QuerySources;

namespace Prototype.ClassifierPrototypeService.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        => services
            .AddQuerySources()
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
#if DEBUG
                    .EnableSensitiveDataLogging()
#endif
                ;
        });
        
        return services;
    }

    private static IServiceCollection AddQuerySources(this IServiceCollection services)
    {
        return services
                .AddScoped<IMovieQuerySource, MovieQuerySource>();

    }
}
