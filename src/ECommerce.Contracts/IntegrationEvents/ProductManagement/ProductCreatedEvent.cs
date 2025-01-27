using ECommerce.SharedFramework;

namespace ECommerce.Contracts.IntegrationEvents.ProductManagement;

public class ProductCreatedEvent :IntegrationEvent
{
    public Guid ProductId { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public Guid CategoryId { get; private set; }

    public ProductCreatedEvent(Guid productId, string title, string description, Guid categoryId)
    {
        EventId = Guid.NewGuid();
        PublishDateTime = DateTime.Now;
        ProductId = productId;
        Title = title;
        Description = description;
        CategoryId = categoryId;
    }


}