using Application.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.Events.Commands.UpdateEvent.UpdateCapacityEvent;

public class UpdateCapacityEventCommandHandler : IRequestHandler<UpdateCapacityEventCommand, string>
{
    private readonly IEventRepository _eventRepository;
    public UpdateCapacityEventCommandHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<string> Handle(UpdateCapacityEventCommand request, CancellationToken cancellationToken)
    {
        var @event = await _eventRepository.GetByIdAsync(request.Id);
        
        if(@event is null)
            throw new NotFoundException("Event", $"Event with id {request.Id} not found");
        
        @event.ChangeCapacity(request.Capacity);
        await _eventRepository.UpdateAsync(@event);
        return @event.Id;
    }
}