using App.Application.Contracts.Persistence;
using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Products;

internal class ProductRepository(AppDbContext context) : 
    GenericRepository<Product, int>(context), IProductRepository
{
    public Task<List<Product>> GetTopPriceProductsAsync(int count)
    {
        return Context.Products.OrderByDescending(p => p.Price).Take(count).ToListAsync();
    }

    public Task<bool> CategoryExistsAsync(int categoryId, CancellationToken cancellationToken)
    {
        return Context.Categories.AnyAsync(c => c.Id == categoryId, cancellationToken);
    }
}