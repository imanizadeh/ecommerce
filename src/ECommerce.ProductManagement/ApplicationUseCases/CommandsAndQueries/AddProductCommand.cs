using MediatR;

namespace ECommerce.ProductManagement.ApplicationUseCases.CommandsAndQueries;

public class AddProductCommand : IRequest<ProductQueryResult>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid CategoryId { get; set; }
}

