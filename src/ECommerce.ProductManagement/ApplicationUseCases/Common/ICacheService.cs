namespace ECommerce.ProductManagement.ApplicationUseCases.Common;

public interface ICacheService
{

    public Task<T?> GetOrSetCollectionsAsync<T>(string cacheKey, TimeSpan expiration, Func<Task<T>>? method = null);
    public Task RemoveKeyAsync(string key);
}