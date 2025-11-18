using Application.Common.Interfaces;
using MediatR;

namespace Application.Events.Commands.DeleteEvent;

public record DeleteEventCommand( 
    string Id 
    ) : IRequest;