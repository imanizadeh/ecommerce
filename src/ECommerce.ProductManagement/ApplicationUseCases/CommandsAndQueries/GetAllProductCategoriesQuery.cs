using MediatR;

namespace ECommerce.ProductManagement.ApplicationUseCases.CommandsAndQueries;

public class GetAllProductCategoriesQuery : BaseGetAllQuery,
    IRequest<List<ProductCategoryQueryResult>>
{

    
}