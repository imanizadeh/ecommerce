namespace ECommerce.Gateway.Dtos.ProductsManagements;

public class AddProductSpecificationDto
{
    public string SpecificationTitle { get; set; }
    public string SpecificationValue { get; set; }
    public int Priority { get; set; }
    public Guid ProductId { get; set; }
}