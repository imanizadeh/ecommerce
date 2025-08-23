using System.Collections;
using EasyCaching.Core;
using ECommerce.ProductManagement.ApplicationUseCases.Common;

namespace ECommerce.ProductManagement.DrivenAdapters.Persistence.Cache;

public class CacheService(IEasyCachingProvider easyCachingProvider) : ICacheService
{
    public async Task<T?> GetOrSetCollectionsAsync<T>(string cacheKey, TimeSpan expiration, Func<Task<T>>? method = null)
    {
        var cached = await easyCachingProvider.GetAsync<T>(cacheKey);
        if (cached.HasValue)
            return cached.Value;
        if (method == null)
        {
            return default;
        }
        var cacheValue = await method();
        if (cacheValue is not null && (cacheValue as ICollection)?.Count > 0)
        {
            await easyCachingProvider.SetAsync(cacheKey, cacheValue, expiration);
        }
        return cacheValue;
    }

    public async Task RemoveKeyAsync(string key)
    {
        await easyCachingProvider.RemoveAsync(key);
    }
}