using MediatR;

namespace ECommerce.ProductManagement.ApplicationUseCases.CommandsAndQueries;

public class EditProductCategoryCommand : IRequest<ProductCategoryQueryResult>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
}
