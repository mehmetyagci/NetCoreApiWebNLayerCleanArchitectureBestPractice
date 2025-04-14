using App.Application.Contracts.Persistence;
using App.Application.Features.Categories.Create;

namespace App.Application.Features.Categories.Validators;

public class CreateCategoryRequestValidator: Application.Features.Categories.Validators.BaseCategoryValidator<CreateCategoryRequest>
{
    public CreateCategoryRequestValidator(ICategoryRepository categoryRepository)
        : base(categoryRepository)
    {
        RuleFor(x => x.Name);
        // .MustAsync(MustHaveUniqueName).WithMessage("Kategori ismi veritabanında bulunmaktadır.");
    }

    // private async Task<bool> MustHaveUniqueName(string name, CancellationToken cancellationToken)
    // {
    //     return !await _categoryRepository
    //         .Where(x => x.Name == name)
    //         .AnyAsync(cancellationToken);
    // }
}