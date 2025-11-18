using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Check.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{
    protected readonly IMediator Mediator;


    public BaseApiController(IMediator mediator)
    {
        Mediator = mediator;
    }
    
    protected async Task<IActionResult> HandleRequest<TRequest, TResponse>(
        TRequest request, 
        CancellationToken cancellationToken = default)
        where TRequest : IRequest<TResponse>
    {
        try
        {
            var result = await Mediator.Send(request, cancellationToken);
            return Ok(result);
        }
        catch (Application.Exceptions.NotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Domain.Exceptions.DomainException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = $"An internal server error occurred. {ex.Message}" });
        }
    }
    
    protected async Task<IActionResult> HandleCommand<TRequest>(
        TRequest request, 
        CancellationToken cancellationToken = default)
        where TRequest : IRequest<string>
    {
        try
        {
            var result = await Mediator.Send(request, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = result }, new { id = result });
        }
        catch (Application.Exceptions.NotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Domain.Exceptions.DomainException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = $"An internal server error occurred. [{ex.Message}]]" });
        }
    }
    
    protected async Task<IActionResult> HandleDeleteCommand<TRequest>(
        TRequest request, 
        CancellationToken cancellationToken = default)
        where TRequest : IRequest
    {
        try
        {
            await Mediator.Send(request, cancellationToken);
            return NoContent();
        }
        catch (Application.Exceptions.NotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Domain.Exceptions.DomainException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = $"An internal server error occurred. {ex.Message}" });
        }
    }

    protected virtual IActionResult GetById(string id)
    {
        return Ok(new { id });
    }
}