using DAL.Repositories;
using DAL.Repositories.Contracts;

namespace DAL.UoW;

public interface IUnitOfWork
{
    IUserProfileRepository UserProfileRepository { get; }
    IUserCommentRepository UserCommentRepository { get; }
    IUserEventCalendarRepository UserEventCalendarRepository { get; }
    
    Task BeginTransactionAsync(CancellationToken cancellationToken);
    Task CommitTransactionAsync(CancellationToken cancellationToken);
    Task RollbackTransactionAsync(CancellationToken cancellationToken);
    Task<int> CompleteAsync(CancellationToken cancellationToken);
}