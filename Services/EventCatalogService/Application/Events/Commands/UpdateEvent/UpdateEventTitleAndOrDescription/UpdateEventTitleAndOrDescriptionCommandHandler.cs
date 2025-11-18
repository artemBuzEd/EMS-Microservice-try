using Domain.Interfaces;
using MediatR;

namespace Application.Events.Commands.UpdateEvent.UpdateEventTitleAndOrDescription;

public class UpdateEventTitleAndOrDescriptionCommandHandler : IRequestHandler<UpdateEventTitleAndOrDescriptionCommand, string>
{
    private readonly IEventRepository _eventRepository;

    public UpdateEventTitleAndOrDescriptionCommandHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<string> Handle(UpdateEventTitleAndOrDescriptionCommand request, CancellationToken cancellationToken)
    {
        var @event = await _eventRepository.GetByIdAsync(request.Id);
        
        if(@event is null)
            throw new NullReferenceException($"Event with id {request.Id} was not found");
        
        @event.UpdateTitle(request.Title);
        @event.UpdateDescription(request.Description);
        await _eventRepository.UpdateAsync(@event);
        return @event.Id;
    }
}