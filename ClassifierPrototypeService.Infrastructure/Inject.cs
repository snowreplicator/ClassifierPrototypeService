﻿using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prototype.ClassifierPrototypeService.Application.Common;
using Prototype.ClassifierPrototypeService.Application.Interfaces.QuerySources;
using Prototype.ClassifierPrototypeService.Application.Interfaces.Repositories;
using Prototype.ClassifierPrototypeService.Infrastructure.common;
using Prototype.ClassifierPrototypeService.Infrastructure.DBContext;
using Prototype.ClassifierPrototypeService.Infrastructure.QuerySources;
using Prototype.ClassifierPrototypeService.Infrastructure.Repositories;

namespace Prototype.ClassifierPrototypeService.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        => services
            .AddQuerySources()
            .AddRepositories(configuration, environment)
            .AddRequestContext();

    private static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        services.AddDbContextFactory<ApplicationDbContext>(o =>
        {
            ServiceOptions serviceOptions = configuration.Get<ServiceOptions>();

            string connectionString = serviceOptions.Db.ConnectionString;
            connectionString = connectionString.TrimEnd(';');

            _ = o.UseNpgsql(connectionString)
                    .UseSchema(serviceOptions.Db.Schema)
                    .EnableSensitiveDataLogging(environment.IsDevelopment())
                ;
        });
        
        services.AddScoped<IMovieRepository, MovieRepository>();

        return services;
    }

    private static IServiceCollection AddQuerySources(this IServiceCollection services)
    {
        services
            .AddScoped<IMovieQuerySource, MovieQuerySource>();
        return services;
    }

    private static IServiceCollection AddRequestContext(this IServiceCollection services)
    {
        services
            .AddScoped<IRequestContext, RequestContext>();
        return services;
    }
}
