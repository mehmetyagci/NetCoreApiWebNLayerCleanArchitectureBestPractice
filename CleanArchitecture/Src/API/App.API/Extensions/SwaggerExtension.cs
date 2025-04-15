﻿namespace App.API.Extensions;

public static class SwaggerExtension
{
    public static IServiceCollection AddSwaggerGenExt(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new() { Title = "CleanArchitecture App.API", Version = "v1" });
        });
        
        return services;
    }

    public static IApplicationBuilder UseSwaggerExt(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        
        return app;
    }
}