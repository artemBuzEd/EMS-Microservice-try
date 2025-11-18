namespace DAL.Entities.HelpModels;

public class UserEventCalendarParameters : QueryStringParameters
{
    public string? user_id { get; set; }
    public string? event_id { get; set; }
}