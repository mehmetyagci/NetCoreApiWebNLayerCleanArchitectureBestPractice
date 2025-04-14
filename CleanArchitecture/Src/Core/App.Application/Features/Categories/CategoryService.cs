﻿using System.Net;
using App.Application.Contracts.Persistence;
using App.Application.Features.Categories.Create;
using App.Application.Features.Categories.Dto;
using App.Application.Features.Categories.Update;
using App.Domain.Entities;
using AutoMapper;

namespace App.Application.Features.Categories;

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
        var category = await categoryRepository.GetCategoryWithProductsAsync();

        var categoryAsDto = mapper.Map<List<CategoryWithProductsDto>>(category);
        
        return ServiceResult<List<CategoryWithProductsDto>>.Success(categoryAsDto);
    }


    public async Task<ServiceResult<List<CategoryDto>>> GetAllListAsync()
    {
        var categories = await categoryRepository.GetAllAsync();
        var categoriesAsDto = mapper.Map<List<CategoryDto>>(categories);
        return ServiceResult<List<CategoryDto>>.Success(categoriesAsDto);
    }

    public async Task<ServiceResult<CategoryDto>> GetByIdAsync(int id)
    {
        var category = await categoryRepository.GetByIdAsync(id);
        if(category is null)
            return ServiceResult<CategoryDto?>.Fail("Category not found!", System.Net.HttpStatusCode.NotFound);

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
        var category = mapper.Map<Category>(request);
        category.Id = id;
        
        categoryRepository.Update(category);
        await unitOfWork.SaveChangesAsync();
        
        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    public async Task<ServiceResult> DeleteAsync(int id)
    {
        var category = await categoryRepository.GetByIdAsync(id);
        categoryRepository.Delete(category!); // NotFoundFilter ile category dolu olması lazım.
        await unitOfWork.SaveChangesAsync();
        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
}