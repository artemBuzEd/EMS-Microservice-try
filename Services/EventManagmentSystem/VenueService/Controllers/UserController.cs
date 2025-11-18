using EMS.DAL.ADO.NET.Entities;
using EMS.DAL.ADO.NET.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace RegistrationService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUOW _uow;
    private ILogger<UserController> _logger;

    public UserController(IUOW uow, ILogger<UserController> logger)
    {
        _uow = uow;
        _logger = logger;
    }

    [HttpGet("GetAllUsers")]
    public async Task<ActionResult<IEnumerable<User>>> GetAllAsync()
    {
        try
        {
            var results = await _uow.UserRepository.GetAllAsync();
            _uow.Commit();
            _logger.LogInformation($"GetAllAsync called");
            return Ok(results);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(500);
        }
    }
    
    [HttpGet("GetByIdAsync/{id:int}", Name = "GetByIdAsync")]
    public async Task<ActionResult<User>> GetByIdAsync(int id)
    {
        try
        {
            var result = await _uow.UserRepository.GetByIdAsync(id);
            _uow.Commit();
            _logger.LogInformation($"GetByIdAsync called");
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(500);
        }
    }
    
    [HttpPost("AddUser")]
    public async Task<ActionResult> PostAsync([FromBody] User user)
    {
        try
        {
            if (user == null)
            {
                _logger.LogError("AddUser called with invalid request");
                return BadRequest();
            }
            var created_id = await _uow.UserRepository.AddAsync(user);
            var result = await _uow.UserRepository.GetByIdAsync(created_id);
            _uow.Commit();
            _logger.LogInformation($"AddAsync called");
            return CreatedAtRoute("GetByIdAsync", new { id = created_id }, result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(500);
        }
    }
    
    [HttpPut("UpdateUser/{id:int}")]
    public async Task<ActionResult> PutAsync(int id,[FromBody] User updatedUser)
    {
        try
        {
            if (updatedUser == null)
            {
                _logger.LogError("UpdateUser called with invalid request");
                return BadRequest();
            }

            var result = await _uow.UserRepository.GetByIdAsync(id);
            if (result == null)
            {
                _logger.LogError("UpdateUser called with invalid request");
                return NotFound();
            }
            
            updatedUser.id = result.id;
            await _uow.UserRepository.UpdateAsync(updatedUser);
            _uow.Commit();
            _logger.LogInformation($"UpdateAsync called");
            return NoContent();
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(500);
        }
    }

    [HttpDelete("DeleteUser/{id:int}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        try
        {
            var result = await _uow.UserRepository.GetByIdAsync(id);
            if (result == null)
            {
                _logger.LogError("DeleteUser called with invalid request");
                return NotFound();
            }

            await _uow.UserRepository.DeleteAsync(id);
            _uow.Commit();
            _logger.LogInformation($"DeleteAsync called");
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(500);
        }
    }
}