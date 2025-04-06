namespace App.Service.Products;

public interface IProductRequest
{
    string Name { get; }
    decimal Price { get; }
    int Stock { get; }
}