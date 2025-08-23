using MediatR;

namespace ECommerce.ProductManagement.ApplicationUseCases.CommandsAndQueries;

public class AddProductSpecificationCommand : IRequest<ProductSpecificationQueryResult>
{
    public string SpecificationTitle { get; set; }
    public string SpecificationValue { get; set; }
    public int Priority { get;  set; }
    public Guid ProductId { get; set; }
}