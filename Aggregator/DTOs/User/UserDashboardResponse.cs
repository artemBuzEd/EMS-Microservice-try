namespace Aggregator.DTOs;

public class UserDashboardResponse
{
    public UserProfileDto User { get; set; }
    /* Needed for Aggregator user dashboard method. Used for managing event Title getting to each comment, and etc.
     
    public IEnumerable<RegisteredEventDto> RegisteredEvents { get; set; } = new List<RegisteredEventDto>();
    public IEnumerable<UserCommentDto> MyComments { get; set; } = new List<UserCommentDto>();
    
    */
    public IEnumerable<CommentDto> MyComments { get; set; }
    public IEnumerable<CalendarDto> MyCalendars { get; set; }
}