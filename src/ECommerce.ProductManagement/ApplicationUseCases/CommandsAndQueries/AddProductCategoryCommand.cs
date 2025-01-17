using MediatR;

namespace ECommerce.ProductManagement.ApplicationUseCases.CommandsAndQueries;

public class AddProductCategoryCommand : IRequest<ProductCategoryQueryResult>
{
    public string Title { get; set; }
}