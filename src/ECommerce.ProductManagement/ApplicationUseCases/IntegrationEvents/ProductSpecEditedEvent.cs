using ECommerce.SharedFramework;

namespace ECommerce.ProductManagement.ApplicationUseCases.IntegrationEvents;

public class ProductSpecEditedEvent : IntegrationEvent
{
    public ProductSpecEditedEvent(Guid specId,
        string specificationTitle,
        string specificationValue,
        int priority,
        Guid productId)
    {
        EventId = Guid.NewGuid();
        PublishDateTime = DateTime.UtcNow;
        SpecId = specId;
        SpecificationTitle = specificationTitle;
        SpecificationValue = specificationValue;
        Priority = priority;
        ProductId = productId;
    }

    public Guid SpecId { get; private set; }
    public string SpecificationTitle { get; private set; }
    public string SpecificationValue { get; private set; }
    public int Priority { get; private set; }
    public Guid ProductId { get; private set; }
}