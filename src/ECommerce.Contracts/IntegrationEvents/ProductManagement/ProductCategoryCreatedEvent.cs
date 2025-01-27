using ECommerce.SharedFramework;

namespace ECommerce.Contracts.IntegrationEvents.ProductManagement;

public class ProductCategoryCreatedEvent : IntegrationEvent
{
    public Guid CategoryId { get; private set; }
    public string Title { get; private set; }

    public ProductCategoryCreatedEvent(Guid categoryId, string title)
    {
        EventId = Guid.NewGuid();
        PublishDateTime = DateTime.Now;
        CategoryId = categoryId;
        Title = title;
    }
}