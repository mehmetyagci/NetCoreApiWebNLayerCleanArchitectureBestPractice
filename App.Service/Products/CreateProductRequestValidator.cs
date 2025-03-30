using FluentValidation;

namespace App.Service.Products;

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotNull().WithMessage("Ürün ismi gereklidir.")
            .NotEmpty().WithMessage("Ürün ismi gereklidir.")
            .Length(3,10).WithMessage("Ürün ismi 3 ile 10 karakter arasında olmalıdır.");

        // price validation
        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("ürün fiyatı 0'dan büyük olmalıdır.");
        
        // stock inclusiveBetween validation
        RuleFor(x => x.Stock)
            .InclusiveBetween(1, 100).WithMessage("stok adedi 1 ile 100 arasında olmalıdır");
        
        



    }
}