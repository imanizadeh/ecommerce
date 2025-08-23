using ECommerce.Catalog.Infrastructure;
using ECommerce.ProductManagement.Contracts;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Catalog.EventHandlers;

public class ProductDeletedEventHandler(
    ApplicationDbContext dbContext,
    ILogger<ProductDeletedEventHandler> logger) : IConsumer<ProductDeletedEvent>
{
    public async Task Consume(ConsumeContext<ProductDeletedEvent> context)
    {
        logger.LogInformation(
            "Handling {ProductDeletedEvent} event: {IntegrationEventId} - ({@IntegrationEvent})",
            nameof(ProductDeletedEvent), context.Message.EventId, context.Message);

        var product = await dbContext.ProductsCatalog
            .FirstOrDefaultAsync(x => x.IntegrationProductId == context.Message.ProductId);

        if (product is not null)
        {
            dbContext.ProductsCatalog.Remove(product);
            await dbContext.SaveChangesAsync();
        }
    }
}