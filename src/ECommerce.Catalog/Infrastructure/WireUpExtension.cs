using ECommerce.Catalog.EventHandlers;
using ECommerce.Contracts.IntegrationEvents.Inventory;
using ECommerce.Contracts.IntegrationEvents.ProductManagement;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Catalog.Infrastructure;

public static class WireUpExtension
{
    public static void RegisterMongoDB(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseMongoDB(builder.Configuration.GetConnectionString("catalogdb"), "catalogdb");
        });
    }

    public static void RegisterMassTransit(this WebApplicationBuilder builder)
    {
        builder.Services.AddMassTransit(x =>
        {
            x.AddConsumer<ProductCategoryCreatedEventHandler>().Endpoint(endpoint =>
            {
                endpoint.ConfigureConsumeTopology = false;
                endpoint.Name = nameof(ProductCategoryCreatedEvent);
            });
            x.AddConsumer<ProductCategoryEditedEventHandler>().Endpoint(endpoint =>
            {
                endpoint.ConfigureConsumeTopology = false;
                endpoint.Name = nameof(ProductCategoryEditedEvent);
            });
            x.AddConsumer<ProductCategoryDeletedEventHandler>().Endpoint(endpoint =>
            {
                endpoint.ConfigureConsumeTopology = false;
                endpoint.Name = nameof(ProductCategoryDeletedEvent);
            });
            
            x.AddConsumer<ProductCreatedEventHandler>().Endpoint(endpoint =>
            {
                endpoint.ConfigureConsumeTopology = false;
                endpoint.Name = nameof(ProductCreatedEvent);
            });
            x.AddConsumer<ProductEditedEventHandler>().Endpoint(endpoint =>
            {
                endpoint.ConfigureConsumeTopology = false;
                endpoint.Name = nameof(ProductEditedEvent);
            });
            x.AddConsumer<ProductDeletedEventHandler>().Endpoint(endpoint =>
            {
                endpoint.ConfigureConsumeTopology = false;
                endpoint.Name = nameof(ProductDeletedEvent);
            });
            x.AddConsumer<ProductSpecCreatedEventHandler>().Endpoint(endpoint =>
            {
                endpoint.ConfigureConsumeTopology = false;
                endpoint.Name = nameof(ProductSpecCreatedEvent);
            });
            x.AddConsumer<ProductSpecEditedEventHandler>().Endpoint(endpoint =>
            {
                endpoint.ConfigureConsumeTopology = false;
                endpoint.Name = nameof(ProductSpecEditedEvent);
            });
            x.AddConsumer<ProductSpecDeletedEventHandler>().Endpoint(endpoint =>
            {
                endpoint.ConfigureConsumeTopology = false;
                endpoint.Name = nameof(ProductSpecDeletedEvent);
            });
            x.AddConsumer<ProductStockCreatedEventHandler>().Endpoint(endpoint =>
            {
                endpoint.ConfigureConsumeTopology = false;
                endpoint.Name = nameof(ProductStockCreatedEvent);
            });
            x.AddConsumer<ProductStockEditedEventHandler>().Endpoint(endpoint =>
            {
                endpoint.ConfigureConsumeTopology = false;
                endpoint.Name = nameof(ProductStockEditedEvent);
            });
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.UseRawJsonSerializer();
                var host = builder.Configuration.GetConnectionString("rabbitmq");
                cfg.Host(host);
                cfg.ConfigureEndpoints(context);
            });
        });
    }
}