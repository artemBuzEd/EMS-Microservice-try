using Application.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.Events.Commands.DeleteEvent;

public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand>
{   
    private readonly IEventRepository _eventRepository;

    public DeleteEventCommandHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }
    
    public async Task Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        var eventToDelete = await _eventRepository.GetByIdAsync(request.Id);
        if(eventToDelete is null)
            throw new NotFoundException("Event", $"Event with id {request.Id} not found");
        
        string id = eventToDelete.Id;
        
        await _eventRepository.DeleteAsync(eventToDelete.Id);
    }
}