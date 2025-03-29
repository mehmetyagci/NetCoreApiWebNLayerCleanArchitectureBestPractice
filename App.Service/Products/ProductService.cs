﻿using App.Repository;
using App.Repository.Products;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace App.Service.Products;

public class ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork) : IProductService
{
    public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count)
    {
        var products = await productRepository.GetTopPriceProductsAsync(count);

        var productDtos = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();

        return new ServiceResult<List<ProductDto>>
        {
            Data = productDtos
        };
    }

    public async Task<ServiceResult<List<ProductDto>>> GetAllListAsync()
    {
        var products = await productRepository.GetAll().ToListAsync();
        var productAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();
        return ServiceResult<List<ProductDto>>.Success(productAsDto);
    }

    public async Task<ServiceResult<List<ProductDto>>> GetPagedAllListAsync(int pageNumber, int pageSize)
    {
        var products = await productRepository.GetAll().Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToListAsync();
        var productAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();
        return ServiceResult<List<ProductDto>>.Success(productAsDto);
    }
    
    public async Task<ServiceResult<ProductDto?>> GetByIdAsync(int id)
    {
        var product = await productRepository.GetByIdAsync(id);

        if (product is null)
            ServiceResult<Product>.Fail("Product not found!", System.Net.HttpStatusCode.NotFound);

        var productAsDto = new ProductDto(product!.Id, product.Name, product.Price, product.Stock);

        return ServiceResult<ProductDto>.Success(productAsDto)!;
    }

    public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request)
    {
        var product = new Product
        {
            Name = request.Name,
            Price = request.Price,
            Stock = request.Stock
        };

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

        product.Name = request.Name;
        product.Price = request.Price;
        product.Stock = request.Stock;

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
