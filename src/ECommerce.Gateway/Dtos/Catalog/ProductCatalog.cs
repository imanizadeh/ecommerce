namespace ECommerce.Gateway.Dtos.Catalog;

public class ProductCatalog
{
    public string Id { get; set; }
    public string Title { get; set; }
    public decimal Price { get;  set; }
    public decimal Discount { get;  set; }
    public string SerialNumber { get; set; }
    public int Count { get; set; }
}