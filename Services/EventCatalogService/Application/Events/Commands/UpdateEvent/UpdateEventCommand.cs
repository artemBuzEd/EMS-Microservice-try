using Application.Common.Interfaces;
using Domain.ValueObjects;

namespace Application.Events.Commands.UpdateEvent;

public record UpdateEventCommand(
    string Id,
    string Title,
    string Description,
    Location Location,
    EventDateRange DateRange,
    int Capacity
    ) : ICommand<string>;