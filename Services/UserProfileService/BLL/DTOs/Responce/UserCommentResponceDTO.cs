namespace BLL.DTOs.Responce;

public class UserCommentResponceDTO
{
    public int id { get; set; }
    public string user_id { get; set; }
    public string event_id { get; set; }
    public string comment { get; set; }
    public int rating { get; set; }
    public DateTime added_at { get; set; }
    public bool is_changed { get; set; } 
}