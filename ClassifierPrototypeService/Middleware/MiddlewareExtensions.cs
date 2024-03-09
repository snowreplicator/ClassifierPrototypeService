using Microsoft.AspNetCore.Builder;

namespace Prototype.ClassifierPrototypeService.Middleware;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseToBaseExceptionWrapping(this IApplicationBuilder app)
    {
        app.UseMiddleware<WrapServiceExceptionsMiddleware>();

        return app;
    }
}
