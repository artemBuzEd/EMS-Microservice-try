using Domain.ValueObjects;
using Application.Common.Interfaces;
namespace Application.Events.Commands.CreateEvent;

public record CreateEventCommand(
    string Title,
    string Description,
    EventDateRange DateRange,
    Location Location,
    string CategoryName,
    string CategoryDescription,
    string OrganizerId,
    string VenueId,
    int Capacity
    ) : ICommand<string>;