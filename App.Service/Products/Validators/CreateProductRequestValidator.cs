using App.Repository.Products;
using App.Service.Products.Create;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using App.Service.Products.Validators.Extensions;

namespace App.Service.Products.Validators;

public class CreateProductRequestValidator : BaseProductValidator<CreateProductRequest>
{
    public CreateProductRequestValidator(IProductRepository productRepository)
        : base(productRepository)
    {
        RuleFor(x => x.Name)
            .ProductNameRules()
            .MustAsync(MustHaveUniqueName)
            .WithMessage("Ürün ismi veritabanında bulunmaktadır.");
    }

    private async Task<bool> MustHaveUniqueName(string name, CancellationToken cancellationToken)
    {
        return !await _productRepository
            .Where(x => x.Name == name)
            .AnyAsync(cancellationToken);
    }
}