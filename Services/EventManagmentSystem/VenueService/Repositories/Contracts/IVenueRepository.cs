using EMS.DAL.ADO.NET.Entities;

namespace EMS.DAL.ADO.NET.Repositories.Contracts;

public interface IVenueRepository : IGenericRepository<Venue>
{
    Task<IEnumerable<Venue>> GetByCityAsync(string city);
}