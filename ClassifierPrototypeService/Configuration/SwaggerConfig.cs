using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Prototype.ClassifierPrototypeService.Configuration;

public static class SwaggerConfig
{
    public static void AddClassifierPrototypeSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(
            options =>
            {
                var xmlCommentsFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFilepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, xmlCommentsFilename);
                options.IncludeXmlComments(xmlCommentsFilepath);
            });
    }
}