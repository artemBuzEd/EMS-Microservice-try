using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Mapster;
using MediatR;

namespace Application.Events.Queries.GetEventsByTitleQuery;

public class GetEventsByTitleQueryHandler : IRequestHandler<GetEventsByTitleQuery, IEnumerable<EventMiniDto>>
{
    private readonly IEventRepository _eventRepository;

    public GetEventsByTitleQueryHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<IEnumerable<EventMiniDto>> Handle(GetEventsByTitleQuery request, CancellationToken cancellationToken)
    {
        var events = await _eventRepository.SearchByTitleAsync(request.Title);
        
        return events.Adapt<IEnumerable<EventMiniDto>>();
    }
}