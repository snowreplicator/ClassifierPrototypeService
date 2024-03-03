using Microsoft.Extensions.DependencyInjection;
using Prototype.ClassifierPrototypeService.Application.ApplicationServices.Interfaces.Queries;
using Prototype.ClassifierPrototypeService.Application.ApplicationServices.Queries;

namespace Prototype.ClassifierPrototypeService.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services
            .AddScoped<IGetMoviesApplicationService, GetMoviesApplicationService>();
    }
}
