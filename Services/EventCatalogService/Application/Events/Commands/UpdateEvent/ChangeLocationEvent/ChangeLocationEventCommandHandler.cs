using Application.Exceptions;
using Domain.Interfaces;
using Domain.ValueObjects;
using MediatR;

namespace Application.Events.Commands.UpdateEvent.ChangeLocationEvent;

public class ChangeLocationEventCommandHandler : IRequestHandler<ChangeLocationEventCommand, string>
{
    private readonly IEventRepository _eventRepository;

    public ChangeLocationEventCommandHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<string> Handle(ChangeLocationEventCommand request, CancellationToken cancellationToken)
    {
        var @event = await _eventRepository.GetByIdAsync(request.Id);
        
        if(@event is null)
            throw new NotFoundException("Event", $"Event with id {request.Id} not found");
        
        @event.ChangeLocation(request.Location);
        await _eventRepository.UpdateAsync(@event);
        return @event.Id;
    }
}