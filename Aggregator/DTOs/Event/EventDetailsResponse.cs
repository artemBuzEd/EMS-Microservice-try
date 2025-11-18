namespace Aggregator.DTOs;

public class EventDetailsResponse
{
    public EventDto Event { get; set; }
    public VenueDto? Venue { get; set; }
    public IEnumerable<CommentDto>? Comments { get; set; } = new List<CommentDto>();
    public int RegisteredUsersCount { get; set; }
}