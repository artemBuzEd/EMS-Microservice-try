namespace DAL.Entities.HelpModels;

public class UserCommentParameters : QueryStringParameters
{
    public string? user_id { get; set; }
    public string? event_id { get; set; }
    public int? rating { get; set; }
}