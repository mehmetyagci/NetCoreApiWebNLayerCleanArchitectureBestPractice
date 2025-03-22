﻿using App.Repository.Products;

namespace App.Service.Products;

public class ProductService(IProductRepository productRepository) : IProductService
{
    public async Task<ServiceResult<List<Product>>> GetTopPriceProductsAsync(int count)
    {
        var products = await productRepository.GetTopPriceProductsAsync(count);
        return new ServiceResult<List<Product>>
        {
            Data = products
        };
    }

    public async Task<ServiceResult<Product>> GetProductByIdAsync(int id)
    {
        var product = await productRepository.GetByIdAsync(id);

        if (product is null)
            ServiceResult<Product>.Fail("Product not found!", System.Net.HttpStatusCode.NotFound);

        return ServiceResult<Product>.Success(product!);
    }
}
