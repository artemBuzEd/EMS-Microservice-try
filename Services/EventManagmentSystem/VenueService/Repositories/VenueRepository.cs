using System.Data;
using System.Data.SqlClient;
using Dapper;
using EMS.DAL.ADO.NET.Entities;
using EMS.DAL.ADO.NET.Repositories.Contracts;

namespace EMS.DAL.ADO.NET.Repositories;

public class VenueRepository : GenericRepository<Venue>, IVenueRepository
{
    public VenueRepository(IDbConnection connection, IDbTransaction transaction) : base(connection, transaction,
        "venues")
    {

    }

    public async Task<IEnumerable<Venue>> GetByCityAsync(string _city)
    {
        string sql = @"SELECT * FROM venues WHERE city = @city";
        
        var results = await _sqlConnection.QueryAsync<Venue>(sql,
            param: new { city = _city },
            transaction: _dbTransaction);
        return results;
    }
    
}