using EMS.DAL.ADO.NET.Entities;

namespace EMS.DAL.ADO.NET.Repositories.Contracts;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User> GetByIdAsync(int id);
    Task<int> AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(int id);
}