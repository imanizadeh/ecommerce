namespace ECommerce.Gateway.Dtos.ProductsManagements;

public class AddProductDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid CategoryId { get; set; }
}