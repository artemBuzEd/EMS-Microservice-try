using DAL.Entities;
using DAL.Entities.HelpModels;
using DAL.Helpers;

namespace DAL.Repositories.Contracts;

public interface IUserProfileRepository : IGenericRepository<UserProfile>
{
    Task<UserProfile?> GetByIdAsync(string id);
    Task<IEnumerable<UserProfile?>> GetByName(string firstName, string lastName);
    Task<UserProfile> GetByIdWithAllComments(string id);
    Task<IEnumerable<UserProfile?>> GetByIdWithAllEventCalendars(string id);
    Task<PagedList<UserProfile>> GetAllPaginatedAsync(UserProfileParameters parameters, ISortHelper<UserProfile> sortHelper);
    
}