using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Service.Products.Create;
using App.Service.Products.Update;

namespace App.Service.Products;

public interface IProductService
{
    Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count);
    Task<ServiceResult<List<ProductDto>>> GetAllListAsync();
    Task<ServiceResult<List<ProductDto>>> GetPagedAllListAsync(int pageNumber, int pageSize);
    Task<ServiceResult<ProductDto?>> GetByIdAsync(int id);
    Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request);
    Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest request);
    Task<ServiceResult> UpdateStockAsync(UpdateProductStockRequest request);
    Task<ServiceResult> DeleteAsync(int id);
}
