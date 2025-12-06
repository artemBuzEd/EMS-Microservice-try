using Application.DTOs;
using Application.Exceptions;
using Domain.Interfaces;
using MediatR;
using Common;
using Domain.Entities;
using Mapster;
using Microsoft.Extensions.Caching.Distributed;

namespace Application.Events.Queries.GetEventByIdQuery;

public class GetEventByIdQueryHandler : IRequestHandler<GetEventByIdQuery, EventDto>
{
    private readonly IEventRepository _eventRepository;

    public GetEventByIdQueryHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<EventDto> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
    {
        var @event = await _eventRepository.GetByIdAsync(request.Id);
        
        if(@event is null)
            throw new NotFoundException("Event", $"Event Id: {request.Id} not found");
        
        return @event.Adapt<EventDto>();
    }
}