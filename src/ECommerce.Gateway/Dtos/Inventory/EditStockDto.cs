namespace ECommerce.Gateway.Dtos.Inventory;

public class EditStockDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public int Count { get; set; }
    public decimal Discount { get; set; }
    public string SerialNumber { get; set; }
    public byte ProductType { get; set; }
    public decimal Price { get; set; }
    public byte Color { get; set; }
}