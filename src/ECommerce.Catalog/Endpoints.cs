using ECommerce.Catalog.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Catalog;

public static class Endpoints
{
    public static void MapInventoryEndpoints(this WebApplication app)
    {
        var inventoryGroup = app.MapGroup("/api/products")
            .WithTags("Products Catalog Api");

        inventoryGroup.MapGet("", async (
            [FromQuery] string? location,
            [FromQuery] bool isAuthenticated,
            [FromQuery] SortType sort,
            [FromServices] CatalogService catalogService) =>
        {
            var query = new GetProductsQuery()
            {
                IsAuthenticated = isAuthenticated,
                Location = location ?? "Iran",
                SortType = sort
            };
            var result = await catalogService.GetProductsAsync(query);
            return TypedResults.Ok(result);
        });
    }
}