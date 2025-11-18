namespace EMS.DAL.ADO.NET.Repositories.Contracts;

public interface IGenericRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task DeleteAsync(int id);
    Task<T> GetByIdAsync(int id);
    Task<int> AddRangeAsync(IEnumerable<T> list);
    Task ReplaceAsync(T t);
    Task<int> AddAsync(T t);
}