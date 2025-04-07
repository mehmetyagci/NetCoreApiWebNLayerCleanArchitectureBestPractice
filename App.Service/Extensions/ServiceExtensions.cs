using System.Reflection;
using App.Service.Categories;
using App.Service.ExceptionHandlers;
using App.Service.Filters;
using App.Service.Products;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Service.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; }); // ASP.NET Core'un default model validation'ını iptal ediyorsun
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();

        services.AddScoped(typeof(NotFoundFilter<,>));
        
        // services.AddFluentValidationAutoValidation(); // FluentValidation ile otomatik model doğrulaması kapatılmıştır. Bu özelliği etkinleştirmek için bu satırın yorumunu kaldırın.
        
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddExceptionHandler<CriticalExceptionHandler>();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        
        return services;
    }
}
