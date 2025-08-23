namespace ECommerce.ProductManagement.ApplicationUseCases.CommandsAndQueries;

public class BaseGetAllQuery
{
    public int PageIndex { get; set; }

    private int _pageSize;

    public int PageSize
    {
        get => _pageSize;
        set
        {
            _pageSize = value > 100 ? 100 : value;
        }
    }
}