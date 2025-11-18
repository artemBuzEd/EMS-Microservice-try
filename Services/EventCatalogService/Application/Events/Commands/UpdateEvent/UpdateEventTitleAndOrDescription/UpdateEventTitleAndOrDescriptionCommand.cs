using Application.Common.Interfaces;

namespace Application.Events.Commands.UpdateEvent.UpdateEventTitleAndOrDescription;

public record UpdateEventTitleAndOrDescriptionCommand(
    string Id,
    string Title,
    string Description
    ): ICommand<string>;