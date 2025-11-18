namespace DAL.Entities.HelpModels;

public class UserProfileParameters : QueryStringParameters
{
    public string? first_name { get; set; }
    public string? last_name { get; set; }
    public DateTime? birth_date { get; set; }
}