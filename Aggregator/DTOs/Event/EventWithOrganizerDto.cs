namespace Aggregator.DTOs;

public class EventWithOrganizerDto
{
    public EventDto Event { get; set; }
    
    public OrganizerDto Organizer { get; set; }
}