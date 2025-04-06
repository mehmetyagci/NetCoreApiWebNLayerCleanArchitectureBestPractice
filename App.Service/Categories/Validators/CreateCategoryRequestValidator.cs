using App.Repository.Categories;
using App.Service.Categories.Create;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace App.Service.Categories.Validators;

public class CreateCategoryRequestValidator: BaseCategoryValidator<CreateCategoryRequest>
{
    public CreateCategoryRequestValidator(ICategoryRepository categoryRepository)
        : base(categoryRepository)
    {
        RuleFor(x => x.Name)
            .MustAsync(MustHaveUniqueName).WithMessage("Kategori ismi veritabanında bulunmaktadır.");
    }

    private async Task<bool> MustHaveUniqueName(string name, CancellationToken cancellationToken)
    {
        return !await _categoryRepository
            .Where(x => x.Name == name)
            .AnyAsync(cancellationToken);
    }
}