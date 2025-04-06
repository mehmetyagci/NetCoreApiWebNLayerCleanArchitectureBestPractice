using App.Repository.Categories;
using App.Service.Categories.Validators.Extensions;
using FluentValidation;

namespace App.Service.Categories.Validators;

public class BaseCategoryValidator<T> : AbstractValidator<T> where T : ICategoryRequest
{
    protected readonly ICategoryRepository _categoryRepository;

    protected BaseCategoryValidator(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;

        ApplyCommonRules();
    }

    private void ApplyCommonRules()
    {
        RuleFor(x => x.Name).CategoryNameRules();
    }
}
