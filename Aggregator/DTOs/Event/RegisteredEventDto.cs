namespace Aggregator.DTOs;

public class RegisteredEventDto
{
    public EventDto Event { get; set; }
    public VenueDto? Venue { get; set; }
    public DateTime AddedAt { get; set; }
    public string RegistrationStatus { get; set; }
}