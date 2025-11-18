using Application.Common.Interfaces;
using Domain.ValueObjects;

namespace Application.Events.Commands.UpdateEvent.ChangeLocationEvent;

public record ChangeLocationEventCommand(
    string Id,
    Location Location
    ) : ICommand<string>;