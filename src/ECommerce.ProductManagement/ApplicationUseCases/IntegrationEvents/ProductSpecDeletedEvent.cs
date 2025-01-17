using ECommerce.SharedFramework;

namespace ECommerce.ProductManagement.ApplicationUseCases.IntegrationEvents;

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