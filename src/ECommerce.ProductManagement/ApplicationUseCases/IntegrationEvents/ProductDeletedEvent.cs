using ECommerce.SharedFramework;

namespace ECommerce.ProductManagement.ApplicationUseCases.IntegrationEvents;

public class ProductDeletedEvent : IntegrationEvent
{
    public Guid ProductId { get; private set; }
    public ProductDeletedEvent(Guid productId)
    {
        PublishDateTime = DateTime.Now;
        EventId = Guid.NewGuid();
        ProductId = productId;
    }
}