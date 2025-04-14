using App.Application.Contracts.Persistence;
using App.Application.Features.Products.Update;
using FluentValidation;

namespace App.Application.Features.Products.Validators;

public class UpdateProductRequestValidator : BaseProductValidator<UpdateProductRequest>
{
    public UpdateProductRequestValidator(IProductRepository productRepository)
        : base(productRepository)
    {
        RuleFor(x => x.Name)
            .MustAsync(HasUniqueNameForOtherRecords).WithMessage("Ürün ismi veritabanında bulunmaktadır.");
    }

    private async Task<bool> HasUniqueNameForOtherRecords(UpdateProductRequest request, string name,
        ValidationContext<UpdateProductRequest> context, CancellationToken cancellationToken)
    {
        if (!context.RootContextData.TryGetValue("Id", out var idObj) || idObj is not int id)
        {
            throw new ValidationException("ID not provided.");
        }

        return !await _productRepository
            .AnyAsync(x => x.Name == name && x.Id != id);
    }
}