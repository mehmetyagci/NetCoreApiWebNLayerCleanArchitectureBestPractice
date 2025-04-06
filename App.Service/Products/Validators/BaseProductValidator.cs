using FluentValidation;
using App.Repository.Products;
using App.Service.Products.Validators.Extensions;

namespace App.Service.Products.Validators;

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
    }
}