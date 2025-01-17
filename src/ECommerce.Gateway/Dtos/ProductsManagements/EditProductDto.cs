namespace ECommerce.Gateway.Dtos.ProductsManagements;

public class EditProductDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid CategoryId { get; set; }
}