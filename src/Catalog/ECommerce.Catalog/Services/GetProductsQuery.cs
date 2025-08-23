namespace ECommerce.Catalog.Services;

public class GetProductsQuery
{
    public string Location { get; set; }
    public bool IsAuthenticated { get; set; }
    public SortType SortType { get; set; }
}