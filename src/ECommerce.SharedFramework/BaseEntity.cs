namespace ECommerce.SharedFramework;

public abstract class BaseEntity<T>
{
    public T Id { get; set; }
}