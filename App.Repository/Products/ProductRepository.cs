using Microsoft.EntityFrameworkCore;

namespace App.Repository.Products;

internal class ProductRepository(AppDbContext context) : GenericRepository<Product>(context), IProductRepository
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