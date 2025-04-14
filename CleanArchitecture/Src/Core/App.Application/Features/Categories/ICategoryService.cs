using App.Application.Features.Categories.Create;
using App.Application.Features.Categories.Dto;
using App.Application.Features.Categories.Update;

namespace App.Application.Features.Categories;

public interface ICategoryService
{
    Task<Service.ServiceResult<CategoryWithProductsDto>> GetCategoryWithProductsAsync(int categoryId);
    Task<Service.ServiceResult<List<CategoryWithProductsDto>>> GetCategoryWithProductsAsync();
    Task<Service.ServiceResult<List<CategoryDto>>> GetAllListAsync();
    Task<Service.ServiceResult<CategoryDto>> GetByIdAsync(int id);
    Task<Service.ServiceResult<CreateCategoryResponse>> CreateAsync(CreateCategoryRequest request);
    Task<Service.ServiceResult> UpdateAsync(int id, UpdateCategoryRequest request);
    Task<Service.ServiceResult> DeleteAsync(int id);
}