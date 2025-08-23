namespace ECommerce.ProductManagement.ApplicationUseCases.CommandsAndQueries;

public class ProductQueryResult
{
    public Guid Id { get; set; }
    public string Title { get;  set; }
    public string Description { get;  set; }
    public Guid CategoryId { get;  set; }
}