using Application.Common.Interfaces;
using Application.DTOs;
using Domain.Helpers;

namespace Application.Events.Queries.GetAllEventsByDateRangeQuery;

public record GetAllEventsByDateRangeQuery(DateTime StartDate, DateTime EndDate) : IQuery<IEnumerable<EventDto>>;