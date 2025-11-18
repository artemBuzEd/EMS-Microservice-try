using Application.Common.Interfaces;
using Application.DTOs;
using Domain.Helpers;

namespace Application.Events.Queries.GetAllEventsQuery;

public record GetAllEventsQuery(
    int PageNumber,
    int PageSize,
    string? CategoryNameFilter
    ): IQuery<PagedResult<EventMiniDto>>;