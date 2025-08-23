using ECommerce.Inventory.ApplicationUseCases.CommandsAndQueries;
using FluentValidation;

namespace ECommerce.Inventory.ApplicationUseCases.Validators;

public class EditStockCommandValidator : AbstractValidator<EditStockCommand>
{
    public EditStockCommandValidator()
    {
        RuleFor(product => product.Id).NotEmpty().WithMessage("Id is required");
        RuleFor(product => product.ProductId).NotEmpty().WithMessage("Product id is required");
        RuleFor(product => product.Count).NotEmpty().GreaterThanOrEqualTo(1).WithMessage("Product count must be greater than 0");
        RuleFor(product => product.SerialNumber).MaximumLength(350).WithMessage("Serial number must not exceed 350 characters");
        RuleFor(product => product.ProductType).NotEmpty().WithMessage("Product type is required");
        RuleFor(product => product.Price).NotEmpty().GreaterThanOrEqualTo(1).WithMessage("Product price must be greater than 0");
        RuleFor(product => product.Color).NotEmpty().WithMessage("Product color is required");
        
    }
}