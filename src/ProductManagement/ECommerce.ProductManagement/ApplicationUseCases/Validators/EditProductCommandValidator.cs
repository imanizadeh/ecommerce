using ECommerce.ProductManagement.ApplicationUseCases.CommandsAndQueries;
using FluentValidation;

namespace ECommerce.ProductManagement.ApplicationUseCases.Validators;

public class EditProductCommandValidator : AbstractValidator<EditProductCommand>
{
    public EditProductCommandValidator()
    {
        RuleFor(product => product.Id).NotEmpty().WithMessage("Product id is required");
        RuleFor(product => product.Title).NotEmpty().WithMessage("Product title is required");
        RuleFor(product => product.Title).MaximumLength(350).WithMessage("Product title must not exceed 350 characters");
        RuleFor(product => product.Description).NotEmpty().WithMessage("Product description is required");
        RuleFor(product => product.Description).MaximumLength(1500).WithMessage("Product description must not exceed 1500 characters");
        RuleFor(product => product.CategoryId).NotEmpty().WithMessage("Product category id is required");
    }
}