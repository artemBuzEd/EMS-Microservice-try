using Application.Common.Interfaces;

namespace Application.Events.Commands.UpdateEvent.UpdateCapacityEvent;

public record UpdateCapacityEventCommand(
    string Id,
    int Capacity
    ) : ICommand<string>;