namespace EMS.DAL.ADO.NET.Entities;

public class Event
{
    public int id { get; set; }
    public string name { get; set; }
    public string? description { get; set; }
    public DateTime starttime { get; set; }
    public DateTime endtime { get; set; }
    public DateTime createdon { get; set; } = DateTime.UtcNow;
    public int venue_id { get; set; }
    public int user_id { get; set; }
}