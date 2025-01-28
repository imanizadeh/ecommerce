using ECommerce.Catalog.Infrastructure;
using ECommerce.Contracts.IntegrationEvents.ProductManagement;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Catalog.EventHandlers;

public class ProductSpecEditedEventHandler(
    ApplicationDbContext dbContext,
    ILogger<ProductSpecEditedEventHandler> logger) : IConsumer<ProductSpecEditedEvent>
{
    public async Task Consume(ConsumeContext<ProductSpecEditedEvent> context)
    {
        logger.LogInformation(
            "Handling {ProductSpecEditedEvent} event: {IntegrationEventId} - ({@IntegrationEvent})",
            nameof(ProductSpecEditedEvent), context.Message.EventId, context.Message);

        var product = await dbContext.ProductsCatalog
            .FirstOrDefaultAsync(x => x.IntegrationProductId == context.Message.ProductId);

        var spec = product.ProductSpecifications
            .FirstOrDefault(x => x.IntegrationSpecificationId == context.Message.SpecId);

        if (product is not null && spec is not null)
        {
            spec.SpecificationTitle = context.Message.SpecificationTitle;
            spec.SpecificationValue = context.Message.SpecificationValue;

            product.ProductSpecifications.Remove(spec);

            product.ProductSpecifications.Add(spec);
            dbContext.ProductsCatalog.Update(product);
            await dbContext.SaveChangesAsync();
        }
       
    }
}