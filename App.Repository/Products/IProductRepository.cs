namespace App.Repository.Products;

public interface IProductRepository : IGenericRepository<Product, int>
{
    public Task<List<Product>> GetTopPriceProductsAsync(int count);
    public Task<bool> CategoryExistsAsync(int categoryId, CancellationToken cancellationToken);
}
