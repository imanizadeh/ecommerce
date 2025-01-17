namespace ECommerce.SharedFramework;

public abstract class IntegrationEvent
{
    public DateTime PublishDateTime { get; protected set; }
    public Guid EventId { get; protected set; }
}