using System.Data;
using System.Data.Common;
using EMS.DAL.ADO.NET.Entities;
using EMS.DAL.ADO.NET.Repositories.Contracts;
using Npgsql;

namespace EMS.DAL.ADO.NET.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDbConnection _sqlConnection; 
    private readonly IDbTransaction _dbTransaction; 
    private readonly string _tableName = "users";
    
    public UserRepository(IDbConnection connection, IDbTransaction transaction) 
    { 
        _sqlConnection = connection;
        _dbTransaction = transaction;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        var results = new List<User>();
        using var command = _sqlConnection.CreateCommand();
        command.CommandText = $"SELECT * FROM {_tableName};";
        command.Transaction = _dbTransaction;
        
        using var reader = await (command as DbCommand).ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            results.Add(MapToUser(reader));
        }
        return results;
    }

    public async Task<User> GetByIdAsync(int id)
    {
        using var command = _sqlConnection.CreateCommand();
        command.CommandText = $"SELECT * FROM {_tableName} WHERE id = @id";
        command.Transaction = _dbTransaction;
        
        var param = command.CreateParameter();
        param.ParameterName = "@id";
        param.Value = id;
        command.Parameters.Add(param);
        
        using var reader = await (command as DbCommand).ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return MapToUser(reader);
        }

        throw new KeyNotFoundException($"User with id [{id}] could not be found.");
    }
    
    public async Task<int> AddAsync(User user)
    {
        if (user == null) throw new ArgumentNullException("User is null");

        using var command = _sqlConnection.CreateCommand();
        command.CommandText = $"INSERT INTO {_tableName} (name, email) VALUES (@Name, @Email) RETURNING id";
        command.Transaction = _dbTransaction;

        var nameParam = command.CreateParameter();
        nameParam.ParameterName = "@Name";
        nameParam.Value = user.name;
        command.Parameters.Add(nameParam);

        var emailParam = command.CreateParameter();
        emailParam.ParameterName = "@Email";
        emailParam.Value = user.email;
        command.Parameters.Add(emailParam);

        return (int)await (command as DbCommand).ExecuteScalarAsync();
    }
    
    public async Task UpdateAsync(User user)
    {
        if (user == null) throw new ArgumentNullException();

        using var command = _sqlConnection.CreateCommand();
        command.CommandText = $"UPDATE {_tableName} SET name = @Name, email = @Email WHERE id = @Id";
        command.Transaction = _dbTransaction;

        var idParam = command.CreateParameter();
        idParam.ParameterName = "@Id";
        idParam.Value = user.id;
        command.Parameters.Add(idParam);

        var nameParam = command.CreateParameter();
        nameParam.ParameterName = "@Name";
        nameParam.Value = user.name;
        command.Parameters.Add(nameParam);

        var emailParam = command.CreateParameter();
        emailParam.ParameterName = "@Email";
        emailParam.Value = user.email;
        command.Parameters.Add(emailParam);

        int rowsAffected = await (command as DbCommand).ExecuteNonQueryAsync();
        if (rowsAffected == 0)
        {
            throw new KeyNotFoundException($"User with id [{user.id}] could not be found.");
        }
    }
    
    public async Task DeleteAsync(int id)
    {
        using var command = _sqlConnection.CreateCommand();
        command.CommandText = $"DELETE FROM {_tableName} WHERE id = @Id";
        command.Transaction = _dbTransaction;

        var param = command.CreateParameter();
        param.ParameterName = "@Id";
        param.Value = id;
        command.Parameters.Add(param);

        int rowsAffected = await (command as DbCommand).ExecuteNonQueryAsync();
        if (rowsAffected == 0)
        {
            throw new KeyNotFoundException($"User with id [{id}] could not be found.");
        }
    }
    
    private User MapToUser(IDataReader reader)
    {
        return new User
        {
            id = (int)reader["id"],
            name = reader["name"] as string,
            email = reader["email"] as string
        };
    }
}