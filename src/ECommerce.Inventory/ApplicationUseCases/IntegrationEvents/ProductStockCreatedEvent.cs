using ECommerce.SharedFramework;

namespace ECommerce.Inventory.ApplicationUseCases.IntegrationEvents;

public class ProductStockCreatedEvent : IntegrationEvent
{
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public int Count { get; private set; }
    public decimal Discount { get; private set; }
    public string SerialNumber { get; private set; }
    public byte ProductType { get; private set; }
    public decimal Price { get; private set; }
    public byte Color { get; private set; }

    public ProductStockCreatedEvent(
        Guid id,
        Guid productId,
        int count,
        decimal discount,
        string serialNumber,
        byte productType,
        decimal price,
        byte color)
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