using FluentValidation;
using App.Repository.Products;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace App.Service.Products.Update;

public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
    private readonly IProductRepository _productRepository;
    
    public UpdateProductRequestValidator(IProductRepository productRepository)
    {
        _productRepository = productRepository;

        RuleFor(x => x.Name)
            .NotNull().WithMessage("Ürün ismi gereklidir.")
            .NotEmpty().WithMessage("ürün ismi gereklidir.")
            .Length(3, 10).WithMessage("Ürün ismi 3 ile 10 karakter arasında olmalıdır.")
            //.Must(MustUniqueProductName).WithMessage("ürün ismi veritabanında bulunmaktadır.");
        //.MustAsync(MustUniqueProductNameAsync).WithMessage("ürün ismi veritabanında bulunmaktadır.");
            .MustAsync(MustUniqueProductNameAsync).WithMessage("Product name must be unique.");

        // price validation
        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("ürün fiyatı 0'dan büyük olmalıdır.");
        
        // stock inclusiveBetween validation
        RuleFor(x => x.Stock)
            .InclusiveBetween(1, 100).WithMessage("stok adedi 1 ile 100 arasında olmalıdır");
    }
    
    private async Task<bool> MustUniqueProductNameAsync(UpdateProductRequest request, string name, ValidationContext<UpdateProductRequest> context, CancellationToken cancellationToken)
    {
        // Id'yi context'ten al
        if (!context.RootContextData.TryGetValue("Id", out var idObj) || idObj is not int id)
        {
            throw new ValidationException("ID not provided."); // Hata fırlat
        }

        // Veritabanındaki aynı isme sahip ve verilen ID dışındaki ürünleri kontrol et
        var anyProduct = await _productRepository
            .Where(x => x.Name == name && x.Id != id) // Adı aynı olan ve id'si farklı olan ürünler
            .AnyAsync(cancellationToken);

        return !anyProduct; // Eğer böyle bir ürün yoksa true, varsa false döner.
    }
}