using Bogus;
using DAL.Entities;

namespace DAL.BogusSeed.Fakers;

public class UserEventCalendarFaker : Faker<UserEventCalendar>
{
    private readonly List<string> _status = new() {"Registered","Interested","Attended"}; 
        
    public UserEventCalendarFaker()
    {
        RuleFor(f => f.event_id, f => f.Random.AlphaNumeric(24));
        RuleFor(f => f.registration_id, f => f.Random.Int(0,100));
        RuleFor(f => f.status, f => f.PickRandom(_status));
    }
}