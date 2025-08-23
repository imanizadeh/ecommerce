using MediatR;

namespace ECommerce.Inventory.ApplicationUseCases.CommandsAndQueries;

public class AddStockCommand : IRequest<StockQueryResult>
{
    public Guid ProductId { get; set; }
    public int Count { get; set; }
    public decimal Discount { get; set; }
    public string SerialNumber { get; set; }
    public byte ProductType { get; set; }
    public decimal Price { get; set; }
    public byte Color { get; set; }
}