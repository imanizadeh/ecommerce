using ECommerce.SharedFramework;

namespace ECommerce.Contracts.IntegrationEvents.ProductManagement;

public class ProductSpecDeletedEvent : IntegrationEvent
{
    public ProductSpecDeletedEvent(Guid specId)
    {
        SpecId = specId;
        EventId = Guid.NewGuid();
        PublishDateTime = DateTime.UtcNow;
    }

    public Guid SpecId { get; private set; }
}