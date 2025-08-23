using ECommerce.Catalog.Infrastructure;
using ECommerce.Catalog.Models;
using ECommerce.ProductManagement.Contracts;
using MassTransit;
using MongoDB.Bson;

namespace ECommerce.Catalog.EventHandlers;

public class ProductCategoryCreatedEventHandler(
    ApplicationDbContext dbContext,
    ILogger<ProductCategoryCreatedEventHandler> logger) :
    IConsumer<ProductCategoryCreatedEvent>
{
    public async Task Consume(ConsumeContext<ProductCategoryCreatedEvent> context)
    {
        logger.LogInformation(
            "Handling {ProductCategoryCreatedEvent} event: {IntegrationEventId} - ({@IntegrationEvent})",
            nameof(ProductCategoryCreatedEvent), context.Message.EventId, context.Message);

        var productCategory = new ProductCategory()
        {
            IntegrationCategoryId = context.Message.CategoryId,
            Title = context.Message.Title,
            Id = ObjectId.GenerateNewId()
        };
        dbContext.ProductCategories.Add(productCategory);
        await dbContext.SaveChangesAsync();
    }
}