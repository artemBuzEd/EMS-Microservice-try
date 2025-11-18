using Application.Common.Interfaces;
using Application.DTOs;

namespace Application.Events.Queries.GetEventByText;

public record GetEventsByTextQuery(
    string Text
    ) : IQuery<IEnumerable<EventMiniDto>>;