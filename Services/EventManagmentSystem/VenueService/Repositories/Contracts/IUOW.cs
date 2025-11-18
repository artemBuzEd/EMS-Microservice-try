namespace EMS.DAL.ADO.NET.Repositories.Contracts;

public interface IUOW
{
    IEventRepository EventRepository { get; }
    IVenueRepository VenueRepository { get; }
    IUserRepository UserRepository { get; }
    
    void Commit();
    void Dispose();
}