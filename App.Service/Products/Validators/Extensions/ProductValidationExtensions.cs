using FluentValidation;

namespace App.Service.Products.Validators.Extensions;

public static class ProductValidationExtensions
{
    public static IRuleBuilderOptions<T, string> ProductNameRules<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty().WithMessage("Ürün ismi gereklidir.")
            .Length(3, 10).WithMessage("Ürün ismi 3 ile 10 karakter arasında olmalıdır.");
    }

    public static IRuleBuilderOptions<T, decimal> ProductPriceRules<T>(this IRuleBuilder<T, decimal> ruleBuilder)
    {
        return ruleBuilder
            .GreaterThan(0).WithMessage("Ürün fiyatı 0'dan büyük olmalıdır.");
    }

    public static IRuleBuilderOptions<T, int> ProductStockRules<T>(this IRuleBuilder<T, int> ruleBuilder)
    {
        return ruleBuilder
            .InclusiveBetween(1, 100).WithMessage("Stok adedi 1 ile 100 arasında olmalıdır.");
    }
}