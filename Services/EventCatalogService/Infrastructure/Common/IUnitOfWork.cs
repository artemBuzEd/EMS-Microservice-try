using MongoDB.Driver;

namespace Infrastructure.Common;

public interface IUnitOfWork : IDisposable
{
    IClientSessionHandle? Session { get; }
    
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task AbortTransactionAsync(CancellationToken cancellationToken = default);
}