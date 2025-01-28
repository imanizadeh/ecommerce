using ECommerce.Gateway.Dtos.Catalog;
using ECommerce.Gateway.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Gateway.EndPoints;

public static class CatalogEndpoints
{
    public static void MapCatalogEndpoints(this WebApplication app)
    {
        var inventoryGroup = app.MapGroup("/api/catalog")
            .WithTags("Catalog Api");
        
        inventoryGroup.MapGet("", async (
            [FromQuery] string? location,
            [FromQuery] bool isAuthenticated,
            [FromQuery] byte sort,
            [FromServices] CatalogService catalogService) =>
        {
            var query = new GetProductQuery()
            {
                IsAuthenticated = isAuthenticated,
                SortType = sort,
                Location = location 
            };
            var productStock = await catalogService.GetProducts(query);
            return TypedResults.Ok(productStock);
        });
    }
}