namespace DAL.Entities;

public class UserEventCalendar
{
    public int id { get; set; }
    public string user_id { get; set; } 
    public string event_id { get; set; } //reference to EventCatalog
    public int? registration_id { get; set; } //reference to Registration Service
    public DateTime added_at { get; set; } = DateTime.UtcNow;
    public string status { get; set; }
    
    public virtual UserProfile user { get; set; }
}