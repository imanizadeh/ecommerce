using ECommerce.SharedFramework;

namespace ECommerce.ProductManagement.ApplicationUseCases.IntegrationEvents;

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