namespace BLL.DTOs.Request.UserProfile;

public class UserProfileUpdateRequestDTO
{
    public string first_name { get; set; }
    public string last_name { get; set; }
    public string bio { get; set; }
    public DateTime birth_date { get; set; }
}