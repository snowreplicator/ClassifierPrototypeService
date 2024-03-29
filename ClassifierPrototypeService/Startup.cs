using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Prototype.ClassifierPrototypeService.Application;
using Prototype.ClassifierPrototypeService.Configuration;
using Prototype.ClassifierPrototypeService.Infrastructure;
using Prototype.ClassifierPrototypeService.Middleware;

namespace Prototype.ClassifierPrototypeService;

public class Startup
{
    private IConfiguration Configuration { get; }
    private IWebHostEnvironment Environment { get; }

    public Startup(IConfiguration configuration, IWebHostEnvironment environment)
    {
        Configuration = configuration;
        Environment = environment;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<ServiceOptions>(Configuration);

        services.AddCors(options =>
        {
            options.AddPolicy("ClassifierPrototypePolicy", builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        services.AddControllers().AddNewtonsoftJson(JsonConfiguration.ConfigureJsonSerializer);

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo {Title = "Classifier prototype", Version = "v1"});
        });
        services.AddClassifierPrototypeSwagger();

        services.AddInfrastructure(Configuration, Environment);
        services.AddApplication();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
    {
        PrintApplicationConfiguration(logger, env);

        if (env.IsDevelopment() || !env.IsDevelopment() && Configuration.Get<ServiceOptions>().UseSwaggerInProduction)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Classifier prototype rest api");
                c.RoutePrefix = "swagger";
            });
        }

        app.UseCors("ClassifierPrototypePolicy");

        app.UseRouting();

        app.UseToBaseExceptionWrapping();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
    
    private void PrintApplicationConfiguration(ILogger<Startup> logger, IHostEnvironment env)
    {
        ServiceOptions serviceOptions = Configuration.Get<ServiceOptions>();
        logger.LogInformation("Application configuration:");
        logger.LogInformation("Environment name: {env.EnvironmentName)}", env.EnvironmentName);
        logger.LogInformation("DbOptions: {serviceOptions?.Db}", serviceOptions?.Db);
        logger.LogInformation("Native locale: {serviceOptions?.NativeLocale}", serviceOptions?.NativeLocale);
        logger.LogInformation("Swagger in production: {serviceOptions?.UseSwaggerInProduction}", serviceOptions?.UseSwaggerInProduction);
    }

}
