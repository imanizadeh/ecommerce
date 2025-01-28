using ECommerce.SharedFramework;

namespace ECommerce.Contracts.IntegrationEvents.Inventory;

public class ProductStockEditedEvent : IntegrationEvent
{
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public int Count { get; private set; }
    public decimal Discount { get; private set; }
    public string SerialNumber { get; private set; }
    public string ProductType { get; private set; }
    public decimal Price { get; private set; }
    public string Color { get; private set; }

    public ProductStockEditedEvent(
        Guid id,
        Guid productId,
        int count,
        decimal discount,
        string serialNumber,
        string productType,
        decimal price,
        string color)
    {
        EventId = Guid.NewGuid();
        PublishDateTime = DateTime.UtcNow;
        Id = id;
        ProductId = productId;
        Count = count;
        Discount = discount;
        SerialNumber = serialNumber;
        ProductType = productType;
        Price = price;
        Color = color;
    }
}