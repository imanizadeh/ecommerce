using MediatR;

namespace ECommerce.ProductManagement.ApplicationUseCases.CommandsAndQueries;

public class GetProductCategoryIdQuery : IRequest<ProductCategoryQueryResult>
{
    public Guid Id { get; set; }
}