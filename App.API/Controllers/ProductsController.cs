﻿using App.Service.Products;
using Microsoft.AspNetCore.Mvc;
using App.Service.Products.Create;
using App.Service.Products.Update;
using App.Service.Products.UpdateStock;

namespace App.API.Controllers;

public class ProductsController(IProductService productService) : CustomBaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll() => CreateActionResult(await productService.GetAllListAsync());
    
    [HttpGet("{pageNumber:int}/{pageSize:int}")]
    public async Task<IActionResult> GetPagedAll(int pageNumber,int pageSize) => CreateActionResult(await 
        productService.GetPagedAllListAsync(pageNumber, pageSize));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id) => CreateActionResult(await productService.GetByIdAsync(id));

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductRequest request) => CreateActionResult(await productService.CreateAsync(request));

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateProductRequest request) => CreateActionResult(await productService.UpdateAsync(id, request));

    // [HttpPut("updatestock")]
    // public async Task<IActionResult> UpdateStockPut(UpdateProductStockRequest request) => CreateActionResult(await productService.UpdateStockAsync(request));

    [HttpPatch("stock")]
    public async Task<IActionResult> UpdateStockPatch(UpdateProductStockRequest request) => CreateActionResult(await productService.UpdateStockAsync(request));
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) => CreateActionResult(await productService.DeleteAsync(id));
}
