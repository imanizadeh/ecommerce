using ECommerce.SharedFramework;

namespace ECommerce.ProductManagement.ApplicationUseCases.IntegrationEvents;

public class ProductCategoryEditedEvent : IntegrationEvent
{
    public Guid CategoryId { get; private set; }
    public string Title { get; private set; }

    public ProductCategoryEditedEvent(Guid categoryId, string title)
    {
        EventId = Guid.NewGuid();
        PublishDateTime = DateTime.Now;
        CategoryId = categoryId;
        Title = title;
    }
}