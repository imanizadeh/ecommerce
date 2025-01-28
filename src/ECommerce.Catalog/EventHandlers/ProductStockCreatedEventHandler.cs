using ECommerce.Catalog.Infrastructure;
using ECommerce.Catalog.Models;
using ECommerce.Contracts.IntegrationEvents.Inventory;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Catalog.EventHandlers;

public class ProductStockCreatedEventHandler(
    ApplicationDbContext dbContext,
    ILogger<ProductStockCreatedEventHandler> logger) : IConsumer<ProductStockCreatedEvent>
{
    public async Task Consume(ConsumeContext<ProductStockCreatedEvent> context)
    {
        logger.LogInformation(
            "Handling {ProductStockCreatedEvent} event: {IntegrationEventId} - ({@IntegrationEvent})",
            nameof(ProductStockCreatedEvent), context.Message.EventId, context.Message);

        var product = await dbContext.ProductsCatalog
            .FirstOrDefaultAsync(x => x.IntegrationProductId == context.Message.ProductId);

        if (product is not null)
        {
            var stock = new ProductStock()
            {
                IntegrationStockId = context.Message.Id,
                Color = context.Message.Color,
                Count = context.Message.Count,
                Discount = context.Message.Discount,
                Price = context.Message.Price,
                ProductType = context.Message.ProductType,
                SerialNumber = context.Message.SerialNumber
            };
            product.ProductStocks.Add(stock);
            dbContext.ProductsCatalog.Update(product);
            await dbContext.SaveChangesAsync();
        }
    }
}