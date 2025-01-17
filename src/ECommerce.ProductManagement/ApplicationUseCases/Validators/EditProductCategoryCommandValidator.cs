using ECommerce.ProductManagement.ApplicationUseCases.CommandsAndQueries;
using FluentValidation;

namespace ECommerce.ProductManagement.ApplicationUseCases.Validators;

public class EditProductCategoryCommandValidator : AbstractValidator<EditProductCategoryCommand>
{
    public EditProductCategoryCommandValidator()
    {
        RuleFor(productCategory => productCategory.Id).NotEmpty().WithMessage("Product category id is required");
        RuleFor(productCategory => productCategory.Title).NotEmpty().WithMessage("Product category title is required");
        RuleFor(productCategory => productCategory.Title).MaximumLength(350).WithMessage("Product category title must not exceed 350 characters");
    }
}