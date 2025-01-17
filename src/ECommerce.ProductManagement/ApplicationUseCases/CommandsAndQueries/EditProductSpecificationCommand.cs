using MediatR;

namespace ECommerce.ProductManagement.ApplicationUseCases.CommandsAndQueries;

public class EditProductSpecificationCommand : IRequest<ProductSpecificationQueryResult>
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public int Priority { get;  set; }
    public string SpecificationTitle{ get; set; }
    public string SpecificationValue { get; set; }
}
