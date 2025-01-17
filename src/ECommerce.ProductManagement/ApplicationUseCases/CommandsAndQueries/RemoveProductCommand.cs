using MediatR;

namespace ECommerce.ProductManagement.ApplicationUseCases.CommandsAndQueries;

public class RemoveProductCommand : IRequest
{
    public Guid Id { get; set; }
}