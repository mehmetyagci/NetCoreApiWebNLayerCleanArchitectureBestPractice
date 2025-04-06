using App.Repository.Categories;
using App.Service.Categories.Update;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace App.Service.Categories.Validators;

public class UpdateCategoryRequestValidator: BaseCategoryValidator<UpdateCategoryRequest>
{
    public UpdateCategoryRequestValidator(ICategoryRepository categoryRepository)
        : base(categoryRepository)
    {
        RuleFor(x => x.Name)
            .MustAsync(HasUniqueNameForOtherRecords).WithMessage("Kategori ismi veritabanında bulunmaktadır.");
    }

    private async Task<bool> HasUniqueNameForOtherRecords(UpdateCategoryRequest request, string name, ValidationContext<UpdateCategoryRequest> context, CancellationToken cancellationToken)
    {
        if (!context.RootContextData.TryGetValue("Id", out var idObj) || idObj is not int id)
        {
            throw new ValidationException("ID not provided.");
        }

        return !await _categoryRepository
            .Where(x => x.Name == name && x.Id != id)
            .AnyAsync(cancellationToken);
    }
}