using ECommerce.SharedFramework;

namespace ECommerce.ProductManagement.Contracts;

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