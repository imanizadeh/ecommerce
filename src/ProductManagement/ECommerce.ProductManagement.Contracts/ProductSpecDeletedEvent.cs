using ECommerce.SharedFramework;

namespace ECommerce.ProductManagement.Contracts;

public class ProductSpecDeletedEvent : IntegrationEvent
{
    public ProductSpecDeletedEvent(Guid specId,Guid productId)
    {
        SpecId = specId;
        EventId = Guid.NewGuid();
        PublishDateTime = DateTime.UtcNow;
        ProductId = productId;
    }
    public Guid SpecId { get; private set; }
    public Guid ProductId { get; private set; }
}