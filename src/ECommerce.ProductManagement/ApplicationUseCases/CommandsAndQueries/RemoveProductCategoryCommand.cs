using MediatR;

namespace ECommerce.ProductManagement.ApplicationUseCases.CommandsAndQueries;

public class RemoveProductCategoryCommand:IRequest
{
    public Guid Id { get; set; }
}