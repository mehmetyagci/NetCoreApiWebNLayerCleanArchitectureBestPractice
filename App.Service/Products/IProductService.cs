using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Service.Products;

public interface IProductService
{
    Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count);
    Task<ServiceResult<List<ProductDto>>> GetAllList();
    Task<ServiceResult<ProductDto?>> GetProductByIdAsync(int id);
    Task<ServiceResult<CreateProductResponse>> CreateProductAsync(CreateProductRequest request);
    Task<ServiceResult> UpdateProductAsync(int id, UpdateProductRequest request);
    Task<ServiceResult> DeleteProductAsync(int id);

}
