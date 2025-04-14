using App.Application.Contracts.Persistence;
using App.Domain.Options;
using App.Persistence.Categories;
using App.Persistence.Interceptors;
using App.Persistence.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Persistence.Extensions;

public static class RepositoryExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<Repository.AppDbContext>(options =>
        {
            var connectionStrings = configuration.GetSection(ConnectionStringOption.Key).Get<ConnectionStringOption>();

            options.UseSqlServer(connectionStrings!.SqlServer, sqlServerOptionsAction =>
            {
                // AppDbContext 'in yeri değişebilir. Migration 'ların Repository 'de kalması için
                // RepositoryAssembly adında bir struct yazdık ve Assembly bilgisini alırken bunu kullandık
                sqlServerOptionsAction.MigrationsAssembly(typeof(PersistenceAssembly).Assembly
                    .FullName); // sqlServerOptionsAction.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
            });

            options.AddInterceptors(new AuditDbContextInterceptors());
        });
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped(typeof(IGenericRepository<,>), typeof(Repository.GenericRepository<,>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}