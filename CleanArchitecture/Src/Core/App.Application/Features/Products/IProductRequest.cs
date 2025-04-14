namespace App.Application.Features.Products;

public interface IProductRequest
{
    string Name { get; }
    decimal Price { get; }
    int Stock { get; }
    int CategoryId { get; }
}