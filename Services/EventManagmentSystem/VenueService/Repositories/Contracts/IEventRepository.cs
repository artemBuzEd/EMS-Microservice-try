using EMS.DAL.ADO.NET.Entities;

namespace EMS.DAL.ADO.NET.Repositories.Contracts;

public interface IEventRepository : IGenericRepository<Event>
{
    Task<IEnumerable<Event>> GetByDateAsync(DateTime startDate, DateTime endDate);
}