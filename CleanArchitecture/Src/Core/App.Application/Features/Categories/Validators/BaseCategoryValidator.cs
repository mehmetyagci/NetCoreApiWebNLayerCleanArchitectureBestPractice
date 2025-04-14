using App.Application.Contracts.Persistence;
using App.Application.Features.Categories.Validators.Extensions;
using FluentValidation;

namespace App.Application.Features.Categories.Validators;

public class BaseCategoryValidator<T> : AbstractValidator<T> where T : Application.Features.Categories.ICategoryRequest
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
