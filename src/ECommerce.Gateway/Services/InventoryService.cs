using System.Text.Json;
using ECommerce.Gateway.Dtos.Inventory;

namespace ECommerce.Gateway.Services;

public class InventoryService(IHttpClientFactory httpClientFactory)
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("Inventory");

    public async Task<Stock> AddProductStock(AddStockDto stockDto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/inventory", stockDto);
        var stock = await ToAModelAsync<Stock>(response);
        return stock;
    }

    public async Task<Stock> EditProductStock(EditStockDto stockDto)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/inventory/{stockDto.Id}", stockDto);
        var stock = await ToAModelAsync<Stock>(response);
        return stock;
    }

    public async Task<Stock> GetStockByIdAsync(Guid id)
    {
        var result = await _httpClient.GetFromJsonAsync<Stock>($"api/inventory/{id}");
        return result;
    }

    public async Task<Stock> GetStockByProductId(Guid productId)
    {
        var result = await _httpClient.GetFromJsonAsync<Stock>($"api/inventory/{productId}/stock");
        return result;
    }

    private async Task<T?> ToAModelAsync<T>(HttpResponseMessage httpResponseMessage)
    {
        httpResponseMessage.EnsureSuccessStatusCode();
        var responseText = await httpResponseMessage.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(responseText, new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
    }
}