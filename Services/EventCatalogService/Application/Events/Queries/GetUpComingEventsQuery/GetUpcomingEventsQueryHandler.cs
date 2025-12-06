using Application.DTOs;
using Application.Exceptions;
using Common;
using Domain.Entities;
using Domain.Helpers;
using Domain.Interfaces;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using MongoDB.Driver;

namespace Application.Events.Queries.GetUpComingEventsQuery;

public class GetUpcomingEventsQueryHandler : IRequestHandler<GetUpcomingEventsQuery, PagedResult<EventDto>>
{
    private readonly IEventRepository _eventRepository;
    private readonly IDistributedCache _cache;

    public GetUpcomingEventsQueryHandler(IEventRepository eventRepository, IDistributedCache cache)
    {
        _eventRepository = eventRepository;
        _cache = cache;
    }

    public async Task<PagedResult<EventDto>> Handle(GetUpcomingEventsQuery request, CancellationToken cancellationToken = default)
    {
        //var events = await _eventRepository.GetUpComingEventsByCategoryAsync(request.CategoryNameFilter, request.PageNumber, request.PageSize);
        string cacheKey = $"events-upcoming-{request.PageNumber}-{request.PageSize}-{request.CategoryNameFilter}";
        var pagedDtos = await _cache.GetOrCreateAsync<PagedResult<EventDto>>(cacheKey, async token =>
        {
            var events = await _eventRepository
                .GetUpComingEventsByCategoryAsync(request.CategoryNameFilter, request.PageNumber, request.PageSize);
            
            return events.Adapt<PagedResult<EventDto>>();
        },
            new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(2)
            });

        if (pagedDtos is null)
            throw new NotFoundException($"Error occured in GetUpcomingEventsQueryHandler on category name {request.CategoryNameFilter} (might be because of CacheAside)");

        return pagedDtos;
    }
}