using Application.Common.Interfaces;
using Application.DTOs;
using Domain.Helpers;

namespace Application.Events.Queries.GetUpComingEventsQuery;

public record GetUpcomingEventsQuery(
    int PageNumber,
    int PageSize,
    string? CategoryNameFilter
    ) : IQuery<PagedResult<EventDto>>;