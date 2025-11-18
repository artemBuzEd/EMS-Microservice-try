using Application.Caching;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Application.Behaviors;

public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : ICacheable
{
    private readonly IMemoryCache _cache;
    private readonly ILogger<CachingBehavior<TRequest, TResponse>> _logger;

    public CachingBehavior(IMemoryCache cache, ILogger<CachingBehavior<TRequest, TResponse>> logger)
    {
        _cache = cache;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        _logger.LogInformation("{requestName} is configured for caching.", requestName);
        
        TResponse response;
        if (_cache.TryGetValue(request.CacheKey, out response))
        {
            _logger.LogInformation("Returning cached value for {Request}", requestName);
            return response;
        }
        
        _logger.LogInformation("{Request} Cached Key: {Key} is not inside the cache, executing request.", requestName, request.CacheKey);
        response = await next();
        _cache.Set(request.CacheKey, response);
        return response;
    }
}