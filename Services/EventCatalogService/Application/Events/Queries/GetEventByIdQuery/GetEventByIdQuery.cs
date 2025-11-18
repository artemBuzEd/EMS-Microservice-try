using Application.Caching;
using Application.Common.Interfaces;
using Application.DTOs;

namespace Application.Events.Queries.GetEventByIdQuery;

public record GetEventByIdQuery(string Id) : IQuery<EventDto>, ICacheable
{
    public string CacheKey => $"GetEventById-{Id}";
    public TimeSpan? CacheDuration => TimeSpan.FromMinutes(2);
}