﻿using App.Repository;
using App.Repository.Products;
using Microsoft.EntityFrameworkCore;
using System.Net;
using App.Service.ExceptionHandlers;
using App.Service.Products.Create;
using App.Service.Products.Dto;
using App.Service.Products.Update;
using App.Service.Products.UpdateStock;
using AutoMapper;

namespace App.Service.Products;

public class ProductService(
    IProductRepository productRepository, 
    IUnitOfWork unitOfWork,
    IMapper mapper) : IProductService
{
    public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count)
    {
        var products = await productRepository.GetTopPriceProductsAsync(count);
        
        var productsAsDto = mapper.Map<List<ProductDto>>(products);

        return new ServiceResult<List<ProductDto>>
        {
            Data = productsAsDto
        };
    }

    public async Task<ServiceResult<List<ProductDto>>> GetAllListAsync()
    {
        var products = await productRepository.GetAll().ToListAsync();
        var productsAsDto = mapper.Map<List<ProductDto>>(products);
        return ServiceResult<List<ProductDto>>.Success(productsAsDto);
    }

    public async Task<ServiceResult<List<ProductDto>>> GetPagedAllListAsync(int pageNumber, int pageSize)
    {
        var products = await productRepository.GetAll().Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToListAsync();
        var productsAsDto = mapper.Map<List<ProductDto>>(products);
        return ServiceResult<List<ProductDto>>.Success(productsAsDto);
    }
    
    public async Task<ServiceResult<ProductDto?>> GetByIdAsync(int id)
    {
        var product = await productRepository.GetByIdAsync(id);
        if (product is null)
          return  ServiceResult<ProductDto?>.Fail("Product not found!", System.Net.HttpStatusCode.NotFound);

        var productAsDto = mapper.Map<ProductDto>(product);
        return ServiceResult<ProductDto>.Success(productAsDto)!;
    }

    public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request)
    {
        // var a = int.Parse("1") / int.Parse("0");
        // throw new CriticalException("hata 1 2 3!");
        // var anyProduct = await productRepository.Where(x => x.Name == request.Name).AnyAsync();
        // if (anyProduct)
        // {
        //     return ServiceResult<CreateProductResponse>.Fail("Product already exists!", 
        //         System.Net.HttpStatusCode.BadRequest);
        // }
        
        var product = mapper.Map<Product>(request);

        await productRepository.AddAsync(product);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult<CreateProductResponse>.SuccessAsCreated(
            new CreateProductResponse(product.Id), $"api/products/{product.Id}");
    }

    public async Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest request)
    {
        var product = await productRepository.GetByIdAsync(id);
        if (product is null)
            return ServiceResult.Fail("Product not found!", System.Net.HttpStatusCode.NotFound);

       
        
        product = mapper.Map(request, product);

        productRepository.Update(product);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    public async Task<ServiceResult> UpdateStockAsync(UpdateProductStockRequest request)
    {
        var product = await productRepository.GetByIdAsync(request.ProductId);

        if (product is null)
        {
            return ServiceResult.Fail("Product not found!", System.Net.HttpStatusCode.NotFound);
        }
        
        product.Stock = request.Quantity;
        
        productRepository.Update(product);
        await unitOfWork.SaveChangesAsync();
        
        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    public async Task<ServiceResult> DeleteAsync(int id)
    {
        var product = await productRepository.GetByIdAsync(id);
        if (product is null)
            return ServiceResult.Fail("Product not found!", System.Net.HttpStatusCode.NotFound);
        productRepository.Delete(product);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
}
