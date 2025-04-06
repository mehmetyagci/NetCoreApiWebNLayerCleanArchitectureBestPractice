using FluentValidation;

namespace App.Service.Categories.Validators.Extensions;

public static class CategoryValidationExtensions
{
    public static IRuleBuilderOptions<T, string> CategoryNameRules<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty().WithMessage("Kategori ismi gereklidir.")
            .Length(1, 150).WithMessage("Kategori ismi 1 ile 150 karakter arasında olmalıdır.");
    }
}