using ECommerce.Catalog.Infrastructure;
using ECommerce.ProductManagement.Contracts;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Catalog.EventHandlers;

public class ProductCategoryDeletedEventHandler(
    ApplicationDbContext dbContext,
    ILogger<ProductCategoryEditedEventHandler> logger) : IConsumer<ProductCategoryDeletedEvent>
{
    public async Task Consume(ConsumeContext<ProductCategoryDeletedEvent> context)
    {
        logger.LogInformation(
            "Handling {ProductCategoryDeletedEvent} event: {IntegrationEventId} - ({@IntegrationEvent})",
            nameof(ProductCategoryDeletedEvent), context.Message.EventId, context.Message);

        var productCategory = await dbContext.ProductCategories
            .FirstOrDefaultAsync(x => x.IntegrationCategoryId == context.Message.CategoryId);

        if (productCategory is not null)
        {
            dbContext.ProductCategories.Remove(productCategory);
            await dbContext.SaveChangesAsync();
        }
    }
}