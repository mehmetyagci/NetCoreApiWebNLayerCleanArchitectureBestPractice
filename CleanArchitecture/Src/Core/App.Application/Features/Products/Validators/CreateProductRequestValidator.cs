using App.Application.Contracts.Persistence;
using App.Application.Features.Products.Create;
using FluentValidation;

namespace App.Application.Features.Products.Validators;

public class CreateProductRequestValidator : BaseProductValidator<CreateProductRequest>
{
    public CreateProductRequestValidator(IProductRepository productRepository)
        : base(productRepository)
    {
        RuleFor(x => x.Name)
            .MustAsync(MustHaveUniqueName).WithMessage("Ürün ismi veritabanında bulunmaktadır.");
    }

    private async Task<bool> MustHaveUniqueName(string name, CancellationToken cancellationToken)
    {
        return !await _productRepository
            .AnyAsync(x => x.Name == name);
    }
}