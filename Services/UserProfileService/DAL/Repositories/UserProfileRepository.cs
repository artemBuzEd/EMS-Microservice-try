using DAL.Entities;
using DAL.Entities.HelpModels;
using DAL.EntityConfig;
using DAL.Helpers;
using DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class UserProfileRepository : GenericRepository<UserProfile>, IUserProfileRepository
{
    public UserProfileRepository(UserProfileDbContext userProfileDbContext) : base(userProfileDbContext)
    {
        
    }

    public async Task<UserProfile?> GetByIdAsync(string id)
    {
        return await _context.UserProfiles.AsNoTracking().FirstOrDefaultAsync(u => u.user_id == id);
    }

    public async Task<IEnumerable<UserProfile?>> GetByName(string firstName, string lastName)
    {
        return await _context.UserProfiles.AsNoTracking().Where(u =>
            u.first_name.ToLower() == firstName.ToLower() && u.last_name.ToLower() == lastName.ToLower()).ToListAsync();
    }

    public async Task<UserProfile> GetByIdWithAllComments(string id)
    {
        var user = await _context.UserProfiles
            .FirstOrDefaultAsync(u => u.user_id == id);

        if (user == null)
        {
            throw new InvalidOperationException($"User not found with this id: {id}");
        }
        
        await _context.UserComments.Where(u => u.user_id == user.user_id).LoadAsync();

        return user;
    }

    public async Task<IEnumerable<UserProfile?>> GetByIdWithAllEventCalendars(string id)
    {
        return await _context.UserProfiles.AsNoTracking()
            .Where(u => u.user_id == id)
            .Include(u => u.EventCalendar).ToListAsync();
    }

    public async Task<PagedList<UserProfile>> GetAllPaginatedAsync(UserProfileParameters parameters, ISortHelper<UserProfile> sortHelper)
    {
        var query = _table.AsQueryable();
        
        if(!string.IsNullOrEmpty(parameters.first_name))
            query = query.Where(u => u.first_name.ToLower().Contains(parameters.first_name.ToLower()));
        
        if(!string.IsNullOrEmpty(parameters.last_name))
            query = query.Where(u => u.last_name.ToLower().Contains(parameters.last_name.ToLower()));
        
        if(parameters.birth_date != null)
            query = query.Where(u => u.birth_date == parameters.birth_date);
        
        query = sortHelper.UseSort(query, parameters.OrderBy);
        
        return await PagedList<UserProfile>.ToPagedListAsync(query, parameters.PageNumber, parameters.PageSize);
    }
}