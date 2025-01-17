using MediatR;

namespace ECommerce.ProductManagement.ApplicationUseCases.CommandsAndQueries;

public class GetProductSpecificationByProductIdQuery : IRequest<List<ProductSpecificationQueryResult>>
{
    public Guid ProductId { get; set; }
}