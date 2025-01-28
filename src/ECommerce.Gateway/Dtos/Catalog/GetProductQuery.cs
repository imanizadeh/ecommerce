namespace ECommerce.Gateway.Dtos.Catalog;

public class GetProductQuery
{
    public string Location { get; set; }
    public bool IsAuthenticated { get; set; }
    public byte SortType { get; set; }
}