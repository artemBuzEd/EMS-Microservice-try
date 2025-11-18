using DAL.Entities;
using DAL.Entities.HelpModels;
using DAL.EntityConfig;
using DAL.Helpers;
using DAL.Repositories.Contracts;
using DAL.Specification;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class UserEventCalendarRepository : GenericRepository<UserEventCalendar>, IUserEventCalendarRepository
{
    public UserEventCalendarRepository(UserProfileDbContext dbContext) : base(dbContext)
    {
        
    }

    public async Task<IEnumerable<UserEventCalendar?>> GetAllEventCalendarsByUserId(string userId)
    {
        return await _context.UserEventCalendars.AsNoTracking()
            .Where(u => u.user_id == userId).ToListAsync();
    }

    public async Task<IEnumerable<UserEventCalendar>> GetAllUserInfoAndEventCalendarsByEventId(string eventId)
    {
        return await _context.UserEventCalendars.Include(u => u.user)
            .Where(u => u.event_id == eventId).AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<UserEventCalendar>> GetAllEventCalendarByRegistrationId(int registrationId)
    {
        return await _context.UserEventCalendars.AsNoTracking()
            .Where(u => u.registration_id == registrationId).ToListAsync();
    }

    public async Task<PagedList<UserEventCalendar>> GetAllPaginatedAsync(UserEventCalendarParameters parameters, ISortHelper<UserEventCalendar> sortHelper)
    {
        var query = _table.AsQueryable();
        
        if(!string.IsNullOrEmpty(parameters.user_id))
            query = query.Where(u => u.user_id.Contains(parameters.user_id));
        
        if(!string.IsNullOrEmpty(parameters.event_id))
            query = query.Where(u => u.event_id == parameters.event_id);
        
        query = sortHelper.UseSort(query, parameters.OrderBy);
        
        return await PagedList<UserEventCalendar>.ToPagedListAsync(query, parameters.PageNumber, parameters.PageSize);
    }

    public IEnumerable<UserEventCalendar> GetAllRegisteredEventCalendars(Specification<UserEventCalendar> specification)
    {
        return SpecificationQueryBuilder
            .BuildQuery(_context.UserEventCalendars, specification)
            .ToList();
    }

    public bool CheckForExistingCalendar(string userId, string eventId)
    {
        if ( _context.UserEventCalendars.Any(u => u.user_id == userId && u.event_id == eventId))
        {
            return true;
        } 
        return false;
    }
}