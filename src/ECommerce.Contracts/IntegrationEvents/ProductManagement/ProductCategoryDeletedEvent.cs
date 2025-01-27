using ECommerce.SharedFramework;

namespace ECommerce.Contracts.IntegrationEvents.ProductManagement;

public class ProductCategoryDeletedEvent : IntegrationEvent
{
    public Guid CategoryId { get; private set; }
    public ProductCategoryDeletedEvent(Guid categoryId)
    {
        EventId = Guid.NewGuid();
        PublishDateTime = DateTime.Now;
        CategoryId = categoryId;
    }
}