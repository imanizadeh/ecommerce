namespace ECommerce.Catalog.Models;

public class ProductStock
{
    public Guid IntegrationStockId { get; set; }
    public int Count { get;  set; }
    public decimal Discount { get;  set; }
    public string SerialNumber { get;  set; }
    public string ProductType { get;  set; }
    public decimal Price { get;  set; }
    public string Color { get;  set; }
}