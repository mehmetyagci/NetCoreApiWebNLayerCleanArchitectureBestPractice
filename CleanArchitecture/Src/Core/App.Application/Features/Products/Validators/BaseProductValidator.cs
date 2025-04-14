using App.Application.Contracts.Persistence;
using App.Application.Features.Products.Validators.Extensions;
using FluentValidation;

namespace App.Application.Features.Products.Validators;

public abstract class BaseProductValidator<T> : AbstractValidator<T> where T : IProductRequest
{
    protected readonly IProductRepository _productRepository;

    protected BaseProductValidator(IProductRepository productRepository)
    {
        _productRepository = productRepository;

        ApplyCommonRules();
    }

    private void ApplyCommonRules()
    {
        RuleFor(x => x.Name).ProductNameRules();
        RuleFor(x => x.Price).ProductPriceRules();
        RuleFor(x => x.Stock).ProductStockRules();
        RuleFor(x => x.CategoryId).ProductCategoryRules(_productRepository);
    }
}