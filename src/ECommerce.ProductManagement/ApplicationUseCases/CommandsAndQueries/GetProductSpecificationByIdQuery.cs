using MediatR;

namespace ECommerce.ProductManagement.ApplicationUseCases.CommandsAndQueries;

public class GetProductSpecificationByIdQuery: IRequest<ProductSpecificationQueryResult>
{
    public Guid Id { get; set; }
}