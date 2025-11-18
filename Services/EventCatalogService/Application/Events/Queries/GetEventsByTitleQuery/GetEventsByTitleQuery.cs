using Application.Common.Interfaces;
using Application.DTOs;
using MediatR;

namespace Application.Events.Queries.GetEventsByTitleQuery;

public record GetEventsByTitleQuery(
    string Title
    ) : IQuery<IEnumerable<EventMiniDto>>;