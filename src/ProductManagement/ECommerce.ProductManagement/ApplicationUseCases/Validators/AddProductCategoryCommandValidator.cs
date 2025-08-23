using ECommerce.ProductManagement.ApplicationUseCases.CommandsAndQueries;
using FluentValidation;

namespace ECommerce.ProductManagement.ApplicationUseCases.Validators;

public class AddProductCategoryCommandValidator : AbstractValidator<AddProductCategoryCommand>
{
    public AddProductCategoryCommandValidator()
    {
        RuleFor(productCategory => productCategory.Title).NotEmpty().WithMessage("Product Title is required");
        RuleFor(productCategory => productCategory.Title).MaximumLength(350).WithMessage("Product Title must not exceed 350 characters");
    }
}