using Microsoft.AspNetCore.Http;

namespace MicroservicesPlatform.ServiceDefaults;

public class CorrelationIdHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CorrelationIdHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext != null)
        {
            var correlationId = httpContext.Request.Headers["X-Correlation-Id"].FirstOrDefault();
            if (!string.IsNullOrEmpty(correlationId))
            {
                request.Headers.Add("X-Correlation-Id", correlationId);
            }
        }
        
        return await base.SendAsync(request, cancellationToken);
    }
}