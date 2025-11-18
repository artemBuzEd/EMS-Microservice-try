using Application.Common.Interfaces;

namespace Application.Events.Commands.UpdateEvent.RescheduleEvent;

public record RescheduleEventCommand(
    string Id,
    DateTime NewStartDate,
    DateTime NewEndDate) : ICommand<string>;