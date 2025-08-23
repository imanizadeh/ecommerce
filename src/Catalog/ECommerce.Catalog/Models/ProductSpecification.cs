namespace ECommerce.Catalog.Models;

public class ProductSpecification
{
    public Guid IntegrationSpecificationId { get; set; }
    public string SpecificationTitle { get; set; }
    public string SpecificationValue { get; set; }
    public int Priority { get; set; }
}