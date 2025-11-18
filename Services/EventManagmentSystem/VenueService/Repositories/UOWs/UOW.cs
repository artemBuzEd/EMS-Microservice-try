using System.Data;
using EMS.DAL.ADO.NET.Repositories.Contracts;

namespace EMS.DAL.ADO.NET.Repositories;

public class UOW : IUOW, IDisposable
{
    public IEventRepository EventRepository { get; }
    public IVenueRepository VenueRepository { get; }
    public IUserRepository UserRepository { get; }
    
    readonly IDbTransaction _dbtransaction;

    public UOW(IEventRepository eventRepository, IVenueRepository venueRepository, IUserRepository userRepository ,IDbTransaction dbtransaction)
    {
        VenueRepository = venueRepository;
        EventRepository = eventRepository;
        UserRepository = userRepository;
        _dbtransaction = dbtransaction;
    }

    public void Commit()
    {
        try
        {
            _dbtransaction.Commit();
        }
        catch (Exception ex)
        {
            _dbtransaction.Rollback();
        }
    }

    public void Dispose()
    {
        _dbtransaction.Connection?.Close();
        _dbtransaction.Connection?.Dispose();
        _dbtransaction.Dispose();
    }
}