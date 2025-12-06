using Application.DTOs;
using Application.Exceptions;
using Common;
using Domain.Interfaces;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace Application.Events.Queries.GetAllEventsByDateRangeQuery;

public class GetAllEventsByDateRangeQueryHandler : IRequestHandler<GetAllEventsByDateRangeQuery, IEnumerable<EventDto>>
{
    private readonly IEventRepository _eventRepository;
    private readonly IDistributedCache _cache;

    public GetAllEventsByDateRangeQueryHandler(IEventRepository eventRepository, IDistributedCache cache)
    {
        _eventRepository = eventRepository;
        _cache = cache;
    }

    public async Task<IEnumerable<EventDto>> Handle(GetAllEventsByDateRangeQuery request, CancellationToken cancellationToken)
    {
        //var events = await _eventRepository.GetEventsByDateAsync(request.StartDate, request.EndDate);
        string cachedKey = $"events-by-date-{request.StartDate}-{request.EndDate}";
        var dtos = await _cache.GetOrCreateAsync<IEnumerable<EventDto>>(cachedKey, async token =>
        {
            var events = await _eventRepository.GetEventsByDateAsync(request.StartDate, request.EndDate);
            return events.Adapt<IEnumerable<EventDto>>();
        },
            new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromMinutes(10)
            });

        if (dtos is null)
            throw new NotFoundException($"Error occured in GetAllEventsByDateRange (might be because of CacheAside)");
        
        return dtos;
    }
}