using Application.Common.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Application.Behaviors;

public class ExceptionRequestLoggingHandler<TRequest, TResponse, TException> : IRequestExceptionHandler<TRequest, TResponse, TException>
where TException : Exception
where TRequest : ICommand<TResponse>
{
    private readonly ILogger<ExceptionRequestLoggingHandler<TRequest, TResponse, TException>> _logger;

    public ExceptionRequestLoggingHandler(ILogger<ExceptionRequestLoggingHandler<TRequest, TResponse, TException>> logger)
    {
        _logger = logger;
    }

    public Task Handle(TRequest request, TException exception, RequestExceptionHandlerState<TResponse> state,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Something went wrong in request {@RequestName}", typeof(TRequest).Name);
        state.SetHandled(default!);
        return Task.CompletedTask;
    }
}