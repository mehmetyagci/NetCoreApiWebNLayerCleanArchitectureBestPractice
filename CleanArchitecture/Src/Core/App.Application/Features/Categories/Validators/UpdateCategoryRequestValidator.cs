 
using App.Application.Contracts.Persistence;
using App.Application.Features.Categories.Update;

namespace App.Application.Features.Categories.Validators;

public class UpdateCategoryRequestValidator: Application.Features.Categories.Validators.BaseCategoryValidator<UpdateCategoryRequest>
{
    public UpdateCategoryRequestValidator(ICategoryRepository categoryRepository)
        : base(categoryRepository)
    {
        RuleFor(x => x.Name);
        //.MustAsync(HasUniqueNameForOtherRecords).WithMessage("Kategori ismi veritabanında bulunmaktadır.");
    }

    // private async Task<bool> HasUniqueNameForOtherRecords(UpdateCategoryRequest request, string name, ValidationContext<UpdateCategoryRequest> context, CancellationToken cancellationToken)
    // {
    //     if (!context.RootContextData.TryGetValue("Id", out var idObj) || idObj is not int id)
    //     {
    //         throw new ValidationException("ID not provided.");
    //     }
    //
    //     return !await _categoryRepository
    //         .Where(x => x.Name == name && x.Id != id)
    //         .AnyAsync(cancellationToken);
    // }
}