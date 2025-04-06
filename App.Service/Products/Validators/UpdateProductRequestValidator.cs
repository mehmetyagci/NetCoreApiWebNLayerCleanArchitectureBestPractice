using App.Repository.Products;
using App.Service.Products.Update;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using App.Service.Products.Validators.Extensions;

namespace App.Service.Products.Validators;

public class UpdateProductRequestValidator : BaseProductValidator<UpdateProductRequest>
{
    public UpdateProductRequestValidator(IProductRepository productRepository)
        : base(productRepository)
    {
        RuleFor(x => x.Name)
            .MustAsync(HasUniqueNameForOtherRecords).WithMessage("Ürün ismi veritabanında bulunmaktadır.");
    }

    private async Task<bool> HasUniqueNameForOtherRecords(UpdateProductRequest request, string name, ValidationContext<UpdateProductRequest> context, CancellationToken cancellationToken)
    {
        if (!context.RootContextData.TryGetValue("Id", out var idObj) || idObj is not int id)
        {
            throw new ValidationException("ID not provided.");
        }

        return !await _productRepository
            .Where(x => x.Name == name && x.Id != id)
            .AnyAsync(cancellationToken);
    }
}