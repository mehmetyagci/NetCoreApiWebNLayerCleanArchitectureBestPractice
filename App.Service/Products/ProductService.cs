using App.Repository.Products;

namespace App.Service.Products;

public class ProductService(IProductRepository productRepository) : IProductService
{

}
