namespace BLL.DTOs.Responce;

public class UserEventCalendarResponceDTO
{
    public int id { get; set; }
    public string user_id { get; set; }
    public string event_id { get; set; }
    public int registration_id { get; set; }
    public DateTime added_at { get; set; }
    public string status { get; set; }
}