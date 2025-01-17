using MediatR;

namespace ECommerce.ProductManagement.ApplicationUseCases.CommandsAndQueries;

public class RemoveProductSpecificationCommand :IRequest
{
    public Guid Id { get; set; }
}