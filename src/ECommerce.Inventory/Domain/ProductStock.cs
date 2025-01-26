using ECommerce.SharedFramework;

namespace ECommerce.Inventory.Domain;

public class ProductStock : BaseEntity<Guid>, IAggregateRoot
{
    public Guid ProductId { get; private set; }
    public int Count { get; private set; }
    public decimal Discount { get; private set; }
    public string SerialNumber { get; private set; }
    public ProductType ProductType { get; private set; }
    public decimal Price { get; private set; }
    public Color Color { get; private set; }

    public ProductStock(Guid productId,
        int count,
        decimal discount,
        string serialNumber,
        ProductType productType,
        decimal price,
        Color color)
    {
        ValidateInventory(productId, count, price);

        ProductId = productId;
        Count = count;
        Discount = discount;
        SerialNumber = serialNumber;
        ProductType = productType;
        Price = price;
        Color = color;
    }


    public void UpdateInventoryData(int count,
        decimal discount,
        string serialNumber,
        ProductType productType,
        decimal price,
        Color color)
    {
        ValidateInventory(count, price);
        
        Count = count;
        Discount = discount;
        SerialNumber = serialNumber;
        ProductType = productType;
        Price = price;
        Color = color;
    }

    private void ValidateInventory(int count, decimal price)
    {
        if (count <= 0)
        {
            throw new DomainException("Count must be greater than 0.");
        }

        if (price <= 0)
        {
            throw new DomainException("Price must be greater than 0.");
        }
    }
    private void ValidateInventory(Guid productId, int count, decimal price)
    {
        if (productId == Guid.Empty)
        {
            throw new DomainException("Product id is required.");
        }

        if (count <= 0)
        {
            throw new DomainException("Count must be greater than 0.");
        }

        if (price <= 0)
        {
            throw new DomainException("Price must be greater than 0.");
        }
    }
}