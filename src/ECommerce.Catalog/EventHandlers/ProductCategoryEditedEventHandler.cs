using ECommerce.Catalog.Infrastructure;
using ECommerce.Catalog.Models;
using ECommerce.Contracts.IntegrationEvents.ProductManagement;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace ECommerce.Catalog.EventHandlers;

public class ProductCategoryEditedEventHandler(
    ApplicationDbContext dbContext,
    ILogger<ProductCategoryEditedEventHandler> logger) : IConsumer<ProductCategoryEditedEvent>
{
    public async Task Consume(ConsumeContext<ProductCategoryEditedEvent> context)
    {
        logger.LogInformation(
            "Handling {ProductCategoryEditedEvent} event: {IntegrationEventId} - ({@IntegrationEvent})",
            nameof(ProductCategoryEditedEvent), context.Message.EventId, context.Message);

        var productCategory = await dbContext.ProductCategories
            .FirstOrDefaultAsync(x => x.IntegrationCategoryId == context.Message.CategoryId);

        if (productCategory is null)
        {
            productCategory = new ProductCategory()
            {
                IntegrationCategoryId = context.Message.CategoryId,
                Title = context.Message.Title,
                Id = ObjectId.GenerateNewId()
            };
            dbContext.ProductCategories.Add(productCategory);
        }
        else
        {
            productCategory.Title = context.Message.Title;
            dbContext.ProductCategories.Update(productCategory);
        }

        await dbContext.SaveChangesAsync();
    }
}