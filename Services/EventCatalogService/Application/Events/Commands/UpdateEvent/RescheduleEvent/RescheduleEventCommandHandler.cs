using Application.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.Events.Commands.UpdateEvent.RescheduleEvent;

public class RescheduleEventCommandHandler : IRequestHandler<RescheduleEventCommand, string>
{
    private readonly IEventRepository _eventRepository;


    public RescheduleEventCommandHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<string> Handle(RescheduleEventCommand request, CancellationToken cancellationToken)
    {
        var @event = await _eventRepository.GetByIdAsync(request.Id);
        
        if(@event is null)
            throw new NotFoundException("Event", $"Event with id {request.Id} not found");
        
        @event.Reschedule(request.NewStartDate, request.NewEndDate);
        await _eventRepository.UpdateAsync(@event);
        return @event.Id;
    }
}