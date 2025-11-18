namespace Check.DTOs.Request;

public record UpdateEventRequest(
    string Title,
    string Description,
    DateTime StartDate,
    DateTime EndDate,
    string Country,
    string City,
    string Address,
    int Capacity
    );