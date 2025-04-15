using App.API.Filters;

namespace App.API.Extensions;

public static class ControllerExtension
{
    public static IServiceCollection AddControllersWithFilterExt(this IServiceCollection services)
    {
        services.AddScoped(typeof(NotFoundFilter<,>));
        
        services.AddControllers(options =>
        {
            options.Filters
                .Add<FluentValidationFilter>(); // FluentValidation filter'ını ekliyorsun (custom async validation için)
            options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes =
                true; // Non-nullable reference type'ların otomatik olarak required yapılmasını engeller (C# 8+ özellik)
        });
        
        return services;
    }
}