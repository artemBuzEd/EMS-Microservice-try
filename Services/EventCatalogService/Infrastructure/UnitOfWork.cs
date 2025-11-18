using Infrastructure.Common;
using MongoDB.Driver;

namespace Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly IMongoClient _mongoClient;
    private IClientSessionHandle? _session;

    public UnitOfWork(IMongoClient mongoClient)
    {
        _mongoClient = mongoClient;
    }
    
    public IClientSessionHandle Session
    {
        get
        {
            if (_session == null)
            {
                _session = _mongoClient.StartSession();
            }
            return _session;
        }
    }

    public void Dispose()
    {
        _session?.Dispose();
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if(_session is null)
            _session = await _mongoClient.StartSessionAsync(cancellationToken: cancellationToken);
        
        _session.StartTransaction();
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if(_session?.IsInTransaction == true)
            await _session.CommitTransactionAsync(cancellationToken);
    }

    public async Task AbortTransactionAsync(CancellationToken cancellationToken = default)
    {
        if(_session?.IsInTransaction == true)
            await _session.AbortTransactionAsync(cancellationToken); 
    }
}