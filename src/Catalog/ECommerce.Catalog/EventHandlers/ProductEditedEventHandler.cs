using ECommerce.Catalog.Infrastructure;
using ECommerce.Catalog.Models;
using ECommerce.ProductManagement.Contracts;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace ECommerce.Catalog.EventHandlers;

public class ProductEditedEventHandler(
    ApplicationDbContext dbContext,
    ILogger<ProductEditedEventHandler> logger) :
    IConsumer<ProductEditedEvent>
{
    public async Task Consume(ConsumeContext<ProductEditedEvent> context)
    {
        logger.LogInformation(
            "Handling {ProductEditedEvent} event: {IntegrationEventId} - ({@IntegrationEvent})",
            nameof(ProductEditedEvent), context.Message.EventId, context.Message);

        var product = await dbContext.ProductsCatalog
            .FirstOrDefaultAsync(x => x.IntegrationProductId == context.Message.ProductId);
        
        var productCategory = await dbContext.ProductCategories
            .FirstOrDefaultAsync(x => x.IntegrationCategoryId == context.Message.CategoryId);
        
        if (product is null)
        {
            product = new ProductCatalog()
            {
                IntegrationCategoryId = context.Message.CategoryId,
                Title = context.Message.Title,
                Id = ObjectId.GenerateNewId(),
                IntegrationProductId = context.Message.ProductId,
                Description = context.Message.Description,
                CategoryTitle = productCategory?.Title ?? string.Empty
            };
            dbContext.ProductsCatalog.Add(product);
        }
        else
        {
            product.Title = context.Message.Title;
            product.Description = context.Message.Description;
            product.CategoryTitle = productCategory?.Title ?? string.Empty;
            product.IntegrationCategoryId = context.Message.CategoryId;
            dbContext.ProductsCatalog.Update(product);
        }

        await dbContext.SaveChangesAsync();
    }
}