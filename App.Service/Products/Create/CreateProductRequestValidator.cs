using App.Repository.Products;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace App.Service.Products.Create;

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    private readonly IProductRepository _productRepository;
    
    public CreateProductRequestValidator(IProductRepository productRepository)
    {
        _productRepository = productRepository;

        RuleFor(x => x.Name)
            .NotNull().WithMessage("Ürün ismi gereklidir.")
            .NotEmpty().WithMessage("ürün ismi gereklidir.")
            .Length(3, 10).WithMessage("Ürün ismi 3 ile 10 karakter arasında olmalıdır.")
            //.Must(MustUniqueProductName).WithMessage("ürün ismi veritabanında bulunmaktadır.");
            .MustAsync(MustUniqueProductNameAsync).WithMessage("ürün ismi veritabanında bulunmaktadır.");

        // price validation
        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("ürün fiyatı 0'dan büyük olmalıdır.");
        
        // stock inclusiveBetween validation
        RuleFor(x => x.Stock)
            .InclusiveBetween(1, 100).WithMessage("stok adedi 1 ile 100 arasında olmalıdır");
    }

    /// <summary>
    /// Asenkron kontrol ama default FluentValidation kapatılmalı.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private  async Task<bool> MustUniqueProductNameAsync(string name, CancellationToken cancellationToken)
    {
        return !await _productRepository.Where(x => x.Name == name).AnyAsync(cancellationToken);
    } 
        
    /// <summary>
    /// Senkron kontrol
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool MustUniqueProductName(string name)
    {
        // false ⇒ bir hata var
        // true ⇒ bir hata yok
        var anyProduct = _productRepository.Where(x => x.Name == name).Any();
        return !anyProduct;
    }
}