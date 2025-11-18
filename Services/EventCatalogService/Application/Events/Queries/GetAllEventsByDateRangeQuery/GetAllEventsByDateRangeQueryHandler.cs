using Application.DTOs;
using Domain.Interfaces;
using Mapster;
using MediatR;

namespace Application.Events.Queries.GetAllEventsByDateRangeQuery;

public class GetAllEventsByDateRangeQueryHandler : IRequestHandler<GetAllEventsByDateRangeQuery, IEnumerable<EventDto>>
{
    private readonly IEventRepository _eventRepository;


    public GetAllEventsByDateRangeQueryHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<IEnumerable<EventDto>> Handle(GetAllEventsByDateRangeQuery request, CancellationToken cancellationToken)
    {
        var events = await _eventRepository.GetEventsByDateAsync(request.StartDate, request.EndDate);
        return events.Adapt<IEnumerable<EventDto>>();
    }
}