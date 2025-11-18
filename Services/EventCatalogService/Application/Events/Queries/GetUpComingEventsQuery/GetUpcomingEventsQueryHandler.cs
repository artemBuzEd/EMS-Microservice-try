using Application.DTOs;
using Domain.Entities;
using Domain.Helpers;
using Domain.Interfaces;
using Mapster;
using MapsterMapper;
using MediatR;
using MongoDB.Driver;

namespace Application.Events.Queries.GetUpComingEventsQuery;

public class GetUpcomingEventsQueryHandler : IRequestHandler<GetUpcomingEventsQuery, PagedResult<EventDto>>
{
    private readonly IEventRepository _eventRepository;

    public GetUpcomingEventsQueryHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<PagedResult<EventDto>> Handle(GetUpcomingEventsQuery request, CancellationToken cancellationToken = default)
    {
        var events = await _eventRepository.GetUpComingEventsByCategoryAsync(request.CategoryNameFilter, request.PageNumber, request.PageSize);
        return events.Adapt<PagedResult<EventDto>>();
    }
}