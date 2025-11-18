using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Events.Commands.CreateEvent;

public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, string>
{ 
    //better to work with dbset rather than with repo
    private readonly IEventRepository _eventRepository;

    public CreateEventCommandHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<string> Handle(CreateEventCommand request, CancellationToken cancellationToken = default)
    {
        var category = new EventCategory(request.CategoryName, request.CategoryDescription);

        var newEvent = new Event(
            request.Title,
            request.Description,
            request.DateRange,
            request.Location,
            category,
            request.OrganizerId,
            request.VenueId,
            request.Capacity
        );
        
        await _eventRepository.AddAsync(newEvent);
        
        return newEvent.Id;
    } 
}