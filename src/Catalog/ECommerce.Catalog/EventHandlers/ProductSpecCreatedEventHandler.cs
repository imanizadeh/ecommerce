using ECommerce.Catalog.Infrastructure;
using ECommerce.Catalog.Models;
using ECommerce.ProductManagement.Contracts;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Catalog.EventHandlers;

public class ProductSpecCreatedEventHandler(
    ApplicationDbContext dbContext,
    ILogger<ProductSpecCreatedEventHandler> logger) : IConsumer<ProductSpecCreatedEvent>
{
    public async Task Consume(ConsumeContext<ProductSpecCreatedEvent> context)
    {
        logger.LogInformation(
            "Handling {ProductSpecCreatedEvent} event: {IntegrationEventId} - ({@IntegrationEvent})",
            nameof(ProductSpecCreatedEvent), context.Message.EventId, context.Message);

        var product = await dbContext.ProductsCatalog
            .FirstOrDefaultAsync(x => x.IntegrationProductId == context.Message.ProductId);
        
        if (product is not null)
        {
            var spec = new ProductSpecification()
            {
                Priority = context.Message.Priority,
                SpecificationTitle = context.Message.SpecificationTitle,
                SpecificationValue = context.Message.SpecificationValue,
                IntegrationSpecificationId =  context.Message.SpecId
            };
            product.ProductSpecifications.Add(spec);
            dbContext.ProductsCatalog.Update(product);
            await dbContext.SaveChangesAsync();
        }
    }
}