using Microsoft.Extensions.DependencyInjection;
using Prototype.ClassifierPrototypeService.Application.ApplicationServices.Commands;
using Prototype.ClassifierPrototypeService.Application.ApplicationServices.Interfaces.Commands;
using Prototype.ClassifierPrototypeService.Application.ApplicationServices.Interfaces.Queries;
using Prototype.ClassifierPrototypeService.Application.ApplicationServices.Queries;

namespace Prototype.ClassifierPrototypeService.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddApplicationServices();

        return services;
    }

    private static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services
            .AddScoped<IGetMoviesApplicationService, GetMoviesApplicationService>()
            .AddScoped<IGetMovieApplicationService, GetMovieApplicationService>()
            .AddScoped<IAddMovieApplicationService, AddMovieApplicationService>();
        return services;
    }
}
