using ECommerce.Gateway.Dtos.Inventory;
using ECommerce.Gateway.Dtos.ProductsManagements;
using ECommerce.Gateway.Services;
using ECommerce.ProductManagement.API.Grpc;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Gateway.EndPoints;

public static class InventoryEndpoints
{
    public static void MapInventoryEndpoints(this WebApplication app)
    {
        var inventoryGroup = app.MapGroup("/api/inventory")
            .WithTags("Inventory Api");

        inventoryGroup.MapPost("", async (
            [FromBody] AddStockDto addStock,
            [FromServices] InventoryService inventoryService) =>
        {
            var productStock = await inventoryService.AddProductStock(addStock);
            return TypedResults.Ok(productStock);
        });

        inventoryGroup.MapPut("{id}", async ([FromBody] EditStockDto editStock,
            [FromRoute] Guid id,
            [FromServices] InventoryService inventoryService) =>
        {
            var productStock = await inventoryService.EditProductStock(editStock);
            return TypedResults.Ok(productStock);
        });
        
        inventoryGroup.MapGet("{id}", async (
            [FromRoute] Guid id,
            [FromServices] InventoryService inventoryService) =>
        {
            var productStock = await inventoryService.GetStockByIdAsync(id);
            return TypedResults.Ok(productStock);
        });
        
        inventoryGroup.MapGet("{productId}/stock", async (
            [FromRoute] Guid productId,
            [FromServices] InventoryService inventoryService) =>
        {
            var productStock = await inventoryService.GetStockByProductId(productId);
            return TypedResults.Ok(productStock);
        });
    }
}