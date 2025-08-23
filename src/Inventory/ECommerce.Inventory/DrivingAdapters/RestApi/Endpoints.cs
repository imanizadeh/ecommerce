using ECommerce.Inventory.ApplicationUseCases.CommandsAndQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Inventory.DrivingAdapters.RestApi;

public static class Endpoints
{
    public static void MapInventoryEndpoints(this WebApplication app)
    {
        var inventoryGroup = app.MapGroup("/api/inventory")
            .WithTags("Inventory Api");
        
        inventoryGroup.MapPost("", async ([FromBody] AddStockCommand addStock,
            [FromServices] IMediator mediator) =>
        {
            var productStock = await mediator.Send(addStock);
            return TypedResults.Ok(productStock);
        });
        
        inventoryGroup.MapPut("{id}", async ([FromBody] EditStockCommand editStock,
            [FromRoute] Guid id,
            [FromServices] IMediator mediator) =>
        {
            var productStock = await mediator.Send(editStock);
            return TypedResults.Ok(productStock);
        });
        
        inventoryGroup.MapGet("{id}", async (
            [FromRoute] Guid id,
            [FromServices] IMediator mediator) =>
        {
            var productStock = await mediator.Send(new GetStockByIdQuery()
            {
                Id = id
            });
            return TypedResults.Ok(productStock);
        });
        
        inventoryGroup.MapGet("{productId}/stock", async (
            [FromRoute] Guid productId,
            [FromServices] IMediator mediator) =>
        {
            var productStock = await mediator.Send(new GetStockByProductIdQuery()
            {
                ProductId = productId
            });
            return TypedResults.Ok(productStock);
        });
    }
}