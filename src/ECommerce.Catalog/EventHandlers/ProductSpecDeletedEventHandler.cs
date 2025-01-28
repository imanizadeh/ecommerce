using ECommerce.Catalog.Infrastructure;
using ECommerce.Contracts.IntegrationEvents.ProductManagement;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Catalog.EventHandlers;

public class ProductSpecDeletedEventHandler(
    ApplicationDbContext dbContext,
    ILogger<ProductSpecDeletedEventHandler> logger) : IConsumer<ProductSpecDeletedEvent>
{
    public async Task Consume(ConsumeContext<ProductSpecDeletedEvent> context)
    {
        logger.LogInformation(
            "Handling {ProductSpecDeletedEvent} event: {IntegrationEventId} - ({@IntegrationEvent})",
            nameof(ProductSpecDeletedEvent), context.Message.EventId, context.Message);

        var product = await dbContext.ProductsCatalog
            .FirstOrDefaultAsync(x => x.IntegrationProductId == context.Message.ProductId);

        var spec = product.ProductSpecifications
            .FirstOrDefault(x => x.IntegrationSpecificationId == context.Message.SpecId);

        if (product is not null && spec is not null)
        {
            product.ProductSpecifications.Remove(spec);
            dbContext.ProductsCatalog.Update(product);
            await dbContext.SaveChangesAsync();
        }
    }
}