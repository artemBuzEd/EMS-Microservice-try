using EMS.DAL.ADO.NET.Entities;
using EMS.DAL.ADO.NET.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace RegistrationService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventController : ControllerBase
{
    private readonly IUOW _uow;
    private ILogger<EventController> _logger;

    public EventController(IUOW uow, ILogger<EventController> logger)
    {
        _uow = uow;
        _logger = logger;
    }

    [HttpGet("GetAllEvents")]
    public async Task<ActionResult<IEnumerable<Event>>> GetAllEvents()
    {
        try
        {
            var result = await _uow.EventRepository.GetAllAsync();
            _uow.Commit();
            _logger.LogInformation("Returned all events");
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message + "Transaction FAILED! Something went wrong in GetAllEvents()");
            return StatusCode(500);
        }
    }

    [HttpGet("GetEventById/{id:int}", Name = "GetEventById")]
    public async Task<ActionResult<Event>> GetEventById(int id)
    {
        try
        {
            var result = await _uow.EventRepository.GetByIdAsync(id);
            _uow.Commit();
            if (result == null)
            {
                _logger.LogInformation($"Returned null event. Event not found with id {id}");
                return NotFound();
            }
            else
            {
                _logger.LogInformation($"Returned event with id {id}");
                return Ok(result);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message + "Event Transaction FAILED! in GetByIdAsync(id)");
            return StatusCode(500);
        }
    }

    [HttpGet("GetEventByDate")]
    public async Task<ActionResult<IEnumerable<Event>>> GetByDateEventsAsync(DateTime startDate, DateTime endDate)
    {
        try
        {
            var result = await _uow.EventRepository.GetByDateAsync(startDate, endDate);
            _uow.Commit();
            if (result == null)
            {
                _logger.LogInformation($"Returned null event. Event not found with date range {startDate} - {endDate}");
                return NotFound();
            }
            else
            {
                _logger.LogInformation($"Returned event with dates range {startDate} - {endDate}");
                return Ok(result);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message + "Event Transaction FAILED! in GetByDateEventsAsync(startDate, endDate)");
            return StatusCode(500);
        }
    }

    [HttpPost("PostEvent")]
    public async Task<ActionResult> PostEventAsync([FromBody] Event e)
    {
        try
        {
            if (e == null)
            {
                _logger.LogError("Event object sent from client is null");
                return BadRequest("Event object sent from client is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Event object sent from client is invalid");
                return BadRequest("Event object sent from client is invalid");
            }

            var created_id = await _uow.EventRepository.AddAsync(e);
            var createdEvent = await _uow.EventRepository.GetByIdAsync(created_id);
            _uow.Commit();
            return CreatedAtRoute("GetEventById", new { id = created_id }, createdEvent);
        }
        catch (Exception ex)
        {
            _logger.LogError($"FAILED POST Event in PostEventAsync(): {ex.Message}");
            return StatusCode(500);
        }
    }

    [HttpPut("Put/{id}")]
    public async Task<ActionResult> PutAsync([FromBody] Event updatedEvent)
    {
        try
        {
            if (updatedEvent == null)
            {
                _logger.LogError("Event object sent from client is null");
                return BadRequest("Event object sent from client is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Event object sent from client is invalid");
                return BadRequest("Event object sent from client is invalid");
            }

            var eventEntity = await _uow.EventRepository.GetByIdAsync(updatedEvent.id);
            if (eventEntity == null)
            {
                _logger.LogError($"Event object with {updatedEvent.id} was not found in the database");
                return BadRequest($"Event object with {updatedEvent.id} was not found in the database");
            }

            await _uow.EventRepository.ReplaceAsync(updatedEvent);
            _uow.Commit();
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError($"FAILED PUT Event in PutAsync(): {ex.Message}");
            return StatusCode(500);
        }
    }

    [HttpDelete("Delete/{id}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        try
        {
            var eventEntity = await _uow.EventRepository.GetByIdAsync(id);
            if (eventEntity == null)
            {
                _logger.LogError($"Event object with {id} was not found in the database");
                return NotFound($"Event object with {id} was not found in the database");
            }

            await _uow.EventRepository.DeleteAsync(id);
            _uow.Commit();
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError($"FAILED DELETE Event in DeleteAsync(): {ex.Message}");
            return StatusCode(500);
        }
    }
}