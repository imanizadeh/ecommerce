using MediatR;

namespace ECommerce.Inventory.ApplicationUseCases.CommandsAndQueries;

public class GetStockByIdQuery : IRequest<StockQueryResult>
{
    public Guid Id { get; set; }
}