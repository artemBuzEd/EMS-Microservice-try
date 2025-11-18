namespace DAL.Entities;

public class UserComment
{
    public int id { get; set; }
    public string user_id { get; set; }
    public string event_id { get; set; }
    public string comment { get; set; }
    public int rating { get; set; }
    public DateTime added_at { get; set; } = DateTime.Now;
    public bool is_changed { get; set; } = false;
    
    public virtual UserProfile user { get; set; }
}