using ECommerce.Catalog.Infrastructure;
using ECommerce.Catalog.Models;
using ECommerce.ProductManagement.Contracts;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace ECommerce.Catalog.EventHandlers;

public class ProductCreatedEventHandler(
    ApplicationDbContext dbContext,
    ILogger<ProductCategoryCreatedEventHandler> logger) : IConsumer<ProductCreatedEvent>
{
    public async Task Consume(ConsumeContext<ProductCreatedEvent> context)
    {
        logger.LogInformation(
            "Handling {ProductCreatedEvent} event: {IntegrationEventId} - ({@IntegrationEvent})",
            nameof(ProductCreatedEvent), context.Message.EventId, context.Message);

        var productCategory = await dbContext.ProductCategories
            .FirstOrDefaultAsync(x => x.IntegrationCategoryId == context.Message.CategoryId);

        var productCatalog = new ProductCatalog()
        {
            IntegrationCategoryId = context.Message.CategoryId,
            Title = context.Message.Title,
            Id = ObjectId.GenerateNewId(),
            Description = context.Message.Description,
            IntegrationProductId = context.Message.ProductId,
            CategoryTitle = productCategory?.Title ?? string.Empty,
        };
        dbContext.ProductsCatalog.Add(productCatalog);
        await dbContext.SaveChangesAsync();
    }
}