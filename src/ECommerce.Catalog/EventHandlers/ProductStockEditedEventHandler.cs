using ECommerce.Catalog.Infrastructure;
using ECommerce.Contracts.IntegrationEvents.Inventory;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Catalog.EventHandlers;

public class ProductStockEditedEventHandler(
    ApplicationDbContext dbContext,
    ILogger<ProductStockEditedEventHandler> logger) : IConsumer<ProductStockEditedEvent>
{
    public async Task Consume(ConsumeContext<ProductStockEditedEvent> context)
    {
        logger.LogInformation(
            "Handling {ProductStockEditedEvent} event: {IntegrationEventId} - ({@IntegrationEvent})",
            nameof(ProductStockEditedEvent), context.Message.EventId, context.Message);

        var product = await dbContext.ProductsCatalog
            .FirstOrDefaultAsync(x => x.IntegrationProductId == context.Message.ProductId);

        var stock = product.ProductStocks
            .FirstOrDefault(x => x.IntegrationStockId == context.Message.Id);

        if (product is not null && stock is not null)
        {
            stock.SerialNumber = context.Message.SerialNumber;
            stock.Price = context.Message.Price;
            stock.Color = context.Message.Color;
            stock.Count = context.Message.Count;
            stock.Discount = context.Message.Discount;
            stock.ProductType = context.Message.ProductType;
            
            product.ProductStocks.Remove(stock);
            product.ProductStocks.Add(stock);
            dbContext.ProductsCatalog.Update(product);
            await dbContext.SaveChangesAsync();
        }
    }
}