using Microsoft.EntityFrameworkCore;

namespace App.Repository.Products;

internal class ProductRepository(AppDbContext context) : GenericRepository<Product>(context), IProductRepository
{
    public Task<List<Product>> GetTopPriceProductsAsync(int count)
    {
        return Context.Products.OrderByDescending(p => p.Price).Take(count).ToListAsync();
    }
}
