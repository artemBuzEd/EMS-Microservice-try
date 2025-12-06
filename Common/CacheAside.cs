using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace Common;

public static class CacheAside
{
    private static readonly DistributedCacheEntryOptions Default = new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15)
    };
    
    public static async Task<T?> GetOrCreateAsync<T>(
        this IDistributedCache cache,
        string key,
        Func<CancellationToken, Task<T>> factory,
        DistributedCacheEntryOptions? options = null,
        ILogger? logger = null,
        CancellationToken cancellationToken = default)
    {
        var cachedValue = await cache.GetStringAsync(key, cancellationToken);

        T? value;
        if (!string.IsNullOrEmpty(cachedValue))
        {
            value = JsonSerializer.Deserialize<T>(cachedValue);

            if (value is not null)
            {
                logger?.LogInformation($"Cache entry for {key}. Value got from cache ");
                return value;
            }
        }
        

        value = await factory(cancellationToken);
        logger?.LogInformation($"Executing factory method");

        if (value is null)
                return default;

        await cache.SetStringAsync(key, JsonSerializer.Serialize(value), options ?? Default, cancellationToken);
        logger?.LogInformation($"Cache entry for {key}. Value stored to cache ");
        
        return value;
    }
}