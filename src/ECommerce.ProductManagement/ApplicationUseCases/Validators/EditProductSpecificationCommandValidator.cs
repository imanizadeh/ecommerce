using ECommerce.ProductManagement.ApplicationUseCases.CommandsAndQueries;
using FluentValidation;

namespace ECommerce.ProductManagement.ApplicationUseCases.Validators;

public class EditProductSpecificationCommandValidator : AbstractValidator<EditProductSpecificationCommand>
{
    public EditProductSpecificationCommandValidator()
    {
        RuleFor(specification => specification.Id).NotEmpty().WithMessage("Specification id is required");
        RuleFor(specification => specification.SpecificationTitle).NotEmpty().WithMessage("Specification title is required");
        RuleFor(specification => specification.SpecificationTitle).MaximumLength(500).WithMessage("Specification title must not exceed 500 characters");
        RuleFor(specification => specification.SpecificationValue).NotEmpty().WithMessage("Specification value is required");
        RuleFor(specification => specification.SpecificationValue).MaximumLength(1000).WithMessage("Specification value must not exceed 1000 characters");
        RuleFor(specification => specification.Priority).GreaterThanOrEqualTo(0).WithMessage("Specification priority must greater than 0");
        RuleFor(specification => specification.ProductId).NotEmpty().WithMessage("product id is required");
    }
}