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

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<ServiceOptions>(Configuration);
        ServiceOptions serviceOptions = Configuration.Get<ServiceOptions>();

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

        services.AddInfrastructure(Configuration);
        services.AddApplication();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
    {
        PrintApplicationConfiguration(logger);
        
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Classifier prototype rest api");
            c.RoutePrefix = "swagger";
        });
        
        app.UseCors("ClassifierPrototypePolicy");

        app.UseRouting();

        app.UseToBaseExceptionWrapping();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
    
    private void PrintApplicationConfiguration(ILogger<Startup> logger)
    {
        ServiceOptions serviceOptions = Configuration.Get<ServiceOptions>();
        logger.LogInformation("Application configuration:");
        logger.LogInformation("DbOptions: '{serviceOptions?.Db}'", serviceOptions?.Db);
        logger.LogInformation("Native locale: '{serviceOptions?.NativeLocale}'", serviceOptions?.NativeLocale);
    }

}
