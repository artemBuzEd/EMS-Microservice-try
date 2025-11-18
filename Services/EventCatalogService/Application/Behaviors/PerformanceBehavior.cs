using System.Diagnostics;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Behaviors;

public class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
where TRequest : ICommand<TResponse>
{
    private readonly ILogger<PerformanceBehavior<TRequest, TResponse>> _logger;
    private readonly Stopwatch _timer;

    public PerformanceBehavior(ILogger<PerformanceBehavior<TRequest, TResponse>> logger, Stopwatch stopwatch)
    {
        _logger = logger;
        _timer = stopwatch;
    }
    
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _timer.Start();
        var result = await next();
        _timer.Stop();
        
        if(_timer.ElapsedMilliseconds > 1000)
            _logger.LogWarning("Long Running Request: {@RequestName} ({@Time} milliseconds)", typeof(TRequest).Name, _timer.ElapsedMilliseconds);
        
        return result;
    }
}