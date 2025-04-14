using System.Reflection;
using App.Application.Features.Categories;
using App.Application.Features.Products;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Application.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; }); // ASP.NET Core'un default model validation'ını iptal ediyorsun
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        
        // services.AddFluentValidationAutoValidation(); // FluentValidation ile otomatik model doğrulaması kapatılmıştır. Bu özelliği etkinleştirmek için bu satırın yorumunu kaldırın.
        
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

 
        
        return services;
    }
}
