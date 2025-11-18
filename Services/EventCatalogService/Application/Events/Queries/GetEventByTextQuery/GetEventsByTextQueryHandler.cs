using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Mapster;
using MediatR;

namespace Application.Events.Queries.GetEventByText;

public class GetEventsByTextQueryHandler : IRequestHandler<GetEventsByTextQuery, IEnumerable<EventMiniDto>>
{
    private readonly IEventRepository _eventRepository;

    public GetEventsByTextQueryHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<IEnumerable<EventMiniDto>> Handle(GetEventsByTextQuery request, CancellationToken cancellationToken)
    {
        var events = await _eventRepository.SearchByTextAsync(request.Text);
        return events.Adapt<IEnumerable<EventMiniDto>>();
    }
}