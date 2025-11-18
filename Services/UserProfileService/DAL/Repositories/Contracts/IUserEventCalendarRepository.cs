using DAL.Entities;
using DAL.Entities.HelpModels;
using DAL.Helpers;
using DAL.Specification;

namespace DAL.Repositories.Contracts;

public interface IUserEventCalendarRepository : IGenericRepository<UserEventCalendar>
{
    Task<IEnumerable<UserEventCalendar>> GetAllEventCalendarsByUserId(string userId);
    Task<IEnumerable<UserEventCalendar>> GetAllUserInfoAndEventCalendarsByEventId(string eventId);
    Task<IEnumerable<UserEventCalendar>> GetAllEventCalendarByRegistrationId(int registrationId);
    Task<PagedList<UserEventCalendar>> GetAllPaginatedAsync(UserEventCalendarParameters parameters, ISortHelper<UserEventCalendar> sortHelper);
    
    IEnumerable<UserEventCalendar> GetAllRegisteredEventCalendars(Specification<UserEventCalendar> specification);

    bool CheckForExistingCalendar(string userId, string eventId);
}