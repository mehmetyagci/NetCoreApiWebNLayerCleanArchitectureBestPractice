using System.Reflection;
using App.Service.ExceptionHandlers;
using App.Service.Products;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Service.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IProductService, ProductService>();

        // services.AddFluentValidationAutoValidation();
        
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddExceptionHandler<CriticalExceptionHandler>();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        
        return services;
    }
}
