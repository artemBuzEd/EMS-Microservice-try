using Application.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.Events.Commands.UpdateEvent;

public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, string>
{
    private readonly IEventRepository _eventRepository;


    public UpdateEventCommandHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }


    public async Task<string> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
    {
        var @event = await _eventRepository.GetByIdAsync(request.Id);

        if (@event is null)
        {
            throw new NotFoundException("Event", $"Event with id {request.Id} not found");
        }

        @event.Update(request.Title, request.Description, request.DateRange.Start, request.DateRange.End, request.Capacity, request.Location);
        
        await _eventRepository.UpdateAsync(@event);
        return @event.Id;
    }
}
