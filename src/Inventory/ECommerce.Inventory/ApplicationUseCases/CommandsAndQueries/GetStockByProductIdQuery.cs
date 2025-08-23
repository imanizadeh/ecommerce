using MediatR;

namespace ECommerce.Inventory.ApplicationUseCases.CommandsAndQueries;

public class GetStockByProductIdQuery : IRequest<StockQueryResult>
{
    public Guid ProductId { get; set; }
}