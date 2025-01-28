using ECommerce.Gateway.Dtos.Catalog;
using ECommerce.Gateway.Dtos.Inventory;

namespace ECommerce.Gateway.Services;

public class CatalogService(IHttpClientFactory httpClientFactory)
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("Catalog");

    public async Task<List<ProductCatalog>> GetProducts(GetProductQuery query)
    {
        var response = await _httpClient.GetFromJsonAsync<List<ProductCatalog>>($"api/products?location={query.Location}&isAuthenticated={query.IsAuthenticated}&sort={query.SortType}");
        return response;
    }
}