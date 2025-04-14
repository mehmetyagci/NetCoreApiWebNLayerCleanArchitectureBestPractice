using System.Net;
using App.Application.Contracts.Persistence;
using App.Application.Features.Categories.Create;
using App.Application.Features.Categories.Dto;
using App.Application.Features.Categories.Update;
using App.Domain.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace App.Application.Features.Categories;

public class CategoryService(
    ICategoryRepository categoryRepository, 
    IUnitOfWork unitOfWork,
    IMapper mapper) : ICategoryService
{
    public async Task<Service.ServiceResult<CategoryWithProductsDto>> GetCategoryWithProductsAsync(int categoryId)
    {
        var category = await categoryRepository.GetCategoryWithProductsAsync(categoryId);

        if (category is null)
        {
            return Service.ServiceResult<CategoryWithProductsDto>.Fail("kategori bulunamadı",
                HttpStatusCode.NoContent);
        }

        var categoryAsDto = mapper.Map<CategoryWithProductsDto>(category);
        
        return Service.ServiceResult<CategoryWithProductsDto>.Success(categoryAsDto);
    }
    
    public async Task<Service.ServiceResult<List<CategoryWithProductsDto>>> GetCategoryWithProductsAsync()
    {
        var category = await categoryRepository.GetCategoryWithProductsAsync();

        var categoryAsDto = mapper.Map<List<CategoryWithProductsDto>>(category);
        
        return Service.ServiceResult<List<CategoryWithProductsDto>>.Success(categoryAsDto);
    }


    public async Task<Service.ServiceResult<List<CategoryDto>>> GetAllListAsync()
    {
        var categories = await categoryRepository.GetAllAsync();
        var categoriesAsDto = mapper.Map<List<CategoryDto>>(categories);
        return Service.ServiceResult<List<CategoryDto>>.Success(categoriesAsDto);
    }

    public async Task<Service.ServiceResult<CategoryDto>> GetByIdAsync(int id)
    {
        var category = await categoryRepository.GetByIdAsync(id);
        if(category is null)
            return Service.ServiceResult<CategoryDto?>.Fail("Category not found!", System.Net.HttpStatusCode.NotFound);

        var categoryAsDto = mapper.Map<CategoryDto>(category);
        return Service.ServiceResult<CategoryDto>.Success(categoryAsDto);
    }
    
    public async Task<Service.ServiceResult<CreateCategoryResponse>> CreateAsync(CreateCategoryRequest request)
    {
        var category = mapper.Map<Category>(request);

        await categoryRepository.AddAsync(category);
        await unitOfWork.SaveChangesAsync();
        return Service.ServiceResult<CreateCategoryResponse>.SuccessAsCreated(
            new CreateCategoryResponse(category.Id), $"api/categories/{category.Id}");
    }

    public async Task<Service.ServiceResult> UpdateAsync(int id, UpdateCategoryRequest request)
    {
        var category = mapper.Map<Category>(request);
        category.Id = id;
        
        categoryRepository.Update(category);
        await unitOfWork.SaveChangesAsync();
        
        return Service.ServiceResult.Success(HttpStatusCode.NoContent);
    }

    public async Task<Service.ServiceResult> DeleteAsync(int id)
    {
        var category = await categoryRepository.GetByIdAsync(id);
        categoryRepository.Delete(category!); // NotFoundFilter ile category dolu olması lazım.
        await unitOfWork.SaveChangesAsync();
        return Service.ServiceResult.Success(HttpStatusCode.NoContent);
    }
}