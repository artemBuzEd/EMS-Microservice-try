namespace BLL.DTOs.Request.UserEventCalendar;

public class UserEventCalendarCreateRequestDTO
{
    public string user_id { get; set; }
    public string event_id { get; set; } 
    public int? registration_id { get; set; } 
    public string status { get; set; }
}