using System.Net;
using App.Repository;
using App.Repository.Categories;
using App.Service.Categories.Create;
using App.Service.Categories.Dto;
using App.Service.Categories.Update;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace App.Service.Categories;

public class CategoryService(
    ICategoryRepository categoryRepository, 
    IUnitOfWork unitOfWork,
    IMapper mapper) : ICategoryService
{
    public async Task<ServiceResult<CategoryWithProductsDto>> GetCategoryWithProductsAsync(int categoryId)
    {
        var category = await categoryRepository.GetCategoryWithProductsAsync(categoryId);

        if (category is null)
        {
            return ServiceResult<CategoryWithProductsDto>.Fail("kategori bulunamadı",
                HttpStatusCode.NoContent);
        }

        var categoryAsDto = mapper.Map<CategoryWithProductsDto>(category);
        
        return ServiceResult<CategoryWithProductsDto>.Success(categoryAsDto);
    }
    
    public async Task<ServiceResult<List<CategoryWithProductsDto>>> GetCategoryWithProductsAsync()
    {
        var category = await categoryRepository.GetCategoryWithProducts().ToListAsync();

        var categoryAsDto = mapper.Map<List<CategoryWithProductsDto>>(category);
        
        return ServiceResult<List<CategoryWithProductsDto>>.Success(categoryAsDto);
    }


    public async Task<ServiceResult<List<CategoryDto>>> GetAllListAsync()
    {
        var categories = await categoryRepository.GetAll().ToListAsync();
        var categoriesAsDto = mapper.Map<List<CategoryDto>>(categories);
        return ServiceResult<List<CategoryDto>>.Success(categoriesAsDto);
    }

    public async Task<ServiceResult<CategoryDto>> GetByIdAsync(int id)
    {
        var category = await categoryRepository.GetByIdAsync(id);
        if(category is null)
            return  ServiceResult<CategoryDto?>.Fail("Category not found!", System.Net.HttpStatusCode.NotFound);

        var categoryAsDto = mapper.Map<CategoryDto>(category);
        return ServiceResult<CategoryDto>.Success(categoryAsDto);
    }
    
    public async Task<ServiceResult<CreateCategoryResponse>> CreateAsync(CreateCategoryRequest request)
    {
        var category = mapper.Map<Category>(request);

        await categoryRepository.AddAsync(category);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult<CreateCategoryResponse>.SuccessAsCreated(
            new CreateCategoryResponse(category.Id), $"api/categories/{category.Id}");
    }

    public async Task<ServiceResult> UpdateAsync(int id, UpdateCategoryRequest request)
    {
        var category = await categoryRepository.GetByIdAsync(id);
        if(category is null)
            return ServiceResult.Fail("Category doesn't exist", HttpStatusCode.NotFound);
        
        category = mapper.Map(request, category);
        
        categoryRepository.Update(category);
        await unitOfWork.SaveChangesAsync();
        
        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    public async Task<ServiceResult> DeleteAsync(int id)
    {
        var category = await categoryRepository.GetByIdAsync(id);

        if (category is null)
        {
            return ServiceResult.Fail("kategori bulunamadı", HttpStatusCode.NotFound);
        }
        
        categoryRepository.Delete(category);
        await unitOfWork.SaveChangesAsync();
        
        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
}