using MediatR;

namespace ECommerce.ProductManagement.ApplicationUseCases.CommandsAndQueries;

public class ProductsQuery : BaseGetAllQuery,
    IRequest<List<ProductQueryResult>>
{
}