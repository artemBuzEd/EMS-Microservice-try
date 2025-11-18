using Application.DTOs;
using Domain.Helpers;
using Domain.Interfaces;
using Mapster;
using MediatR;

namespace Application.Events.Queries.GetAllEventsQuery;

public class GetAllEventsQueryHandler : IRequestHandler<GetAllEventsQuery, PagedResult<EventMiniDto>>
{
    private readonly IEventRepository _eventRepository;

    public GetAllEventsQueryHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }
    
    public async Task<PagedResult<EventMiniDto>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
    {
        var events = await _eventRepository.GetPagedEventsAsync(request.PageNumber, request.PageSize, request.CategoryNameFilter);
        return events.Adapt<PagedResult<EventMiniDto>>();
    }
    
    
}