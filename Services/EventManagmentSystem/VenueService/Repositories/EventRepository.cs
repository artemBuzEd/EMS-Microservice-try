using System.Data;
using Dapper;
using EMS.DAL.ADO.NET.Entities;
using EMS.DAL.ADO.NET.Repositories.Contracts;

namespace EMS.DAL.ADO.NET.Repositories;

public class EventRepository : GenericRepository<Event>, IEventRepository
{
    public EventRepository(IDbConnection sqlConnection, IDbTransaction dbTransaction) : base(sqlConnection, dbTransaction, "events")
    {
    }

    public async Task<IEnumerable<Event>> GetByDateAsync(DateTime startDate, DateTime endDate)
    {
        string sql = @"SELECT * FROM events WHERE starttime BETWEEN @StartDate AND @EndDate";
        
        var results = await _sqlConnection.QueryAsync<Event>(sql,
            param: new { StartDate = startDate, EndDate = endDate },
            transaction: _dbTransaction);
        return results;
    }
}