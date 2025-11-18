using DAL.Entities;

namespace DAL.Specification;

public class RegisteredEventCalendarsSpecification : Specification<UserEventCalendar>
{
    public RegisteredEventCalendarsSpecification(string eventId) : base(u => u.status == "Registered" && eventId == u.event_id)
    {
        
    }
}