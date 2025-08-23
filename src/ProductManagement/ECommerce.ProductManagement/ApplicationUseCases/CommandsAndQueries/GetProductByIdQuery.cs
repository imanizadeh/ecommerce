using MediatR;

namespace ECommerce.ProductManagement.ApplicationUseCases.CommandsAndQueries;

public class GetProductByIdQuery : IRequest<ProductQueryResult>
{
    public Guid Id { get; set; }
}