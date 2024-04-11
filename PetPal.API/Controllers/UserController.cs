using PetPalApp.Domain;
using PetPalApp.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace PetPal.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
  private readonly IUserService userService;
  private readonly ILogger<UserController> logger;
  public UserController(IUserService _userService, ILogger<UserController> _logger) 
  {
    userService = _userService;
    logger = _logger;
  }

  [Authorize]
  [HttpGet]
  public ActionResult<List<UserDTO>> GetAll()
  {
    try
    {
      var users = userService.GetAllUsers(User.FindFirst(ClaimTypes.Role)?.Value);
      return Ok(users);
    }
    catch (KeyNotFoundException knfex)
    {
      return NotFound($"No users found: {knfex.Message}");
    }
    catch (UnauthorizedAccessException uaex)
    {
      return Unauthorized(uaex.Message);
    }
    catch (Exception ex)
    {
      return BadRequest($"Error getting all users: {ex.Message}");
    }
  }
  [Authorize]
  [HttpGet("{userId}")]
  public ActionResult<UserDTO> Get(int userId)
  {
    try
    {
      var user = userService.GetUser(User.FindFirst(ClaimTypes.Role)?.Value, User.FindFirst(ClaimTypes.NameIdentifier)?.Value, userId);
      return Ok(user);
    }
    catch (KeyNotFoundException knfex)
    {
      return NotFound($"User {userId} not found: {knfex.Message}");
    }
    catch (UnauthorizedAccessException uaex)
    {
      return Unauthorized(uaex.Message);
    }
    catch (Exception ex)
    {
      return BadRequest($"Error getting user: {ex.Message}");
    }
  }

  [HttpPost]
  public ActionResult<User> Create([FromBody] UserCreateUpdateDTO userCreateUpdateDTO)
  {
    if (!ModelState.IsValid) return BadRequest(ModelState);
    try
    {
      var user = userService.RegisterUser(userCreateUpdateDTO);
      return CreatedAtAction(nameof(Get), new { userId = user.UserId }, user);
    }
    catch (System.Text.Json.JsonException jex)
    {
      return BadRequest($"Invalid JSON format: {jex.Message}");
    }
    catch (Exception ex)
    {
      return BadRequest(ex.Message);
    }
  }
  [Authorize]
  [HttpPut("{userId}")]
  public IActionResult Update(int userId, [FromBody] UserCreateUpdateDTO userCreateUpdateDTO)
  {
    if (!ModelState.IsValid) return BadRequest(ModelState);
    try
    {
      userService.UpdateUser(User.FindFirst(ClaimTypes.Role)?.Value, User.FindFirst(ClaimTypes.NameIdentifier)?.Value, userId, userCreateUpdateDTO);
      return Ok(userCreateUpdateDTO);
    }
    catch (KeyNotFoundException nfex)
    {
      return NotFound($"User {userId} not found: {nfex.Message}");
    }
    catch (System.Text.Json.JsonException jex)
    {
      return BadRequest($"Invalid JSON format: {jex.Message}");
    }
    catch (UnauthorizedAccessException uaex)
    {
      return Unauthorized(uaex.Message);
    }
    catch (Exception ex)
    {
      return BadRequest($" Error updating user data: {ex.Message}");
    }
  }
  [Authorize]
  [HttpDelete("{userId}")]
  public IActionResult Delete(int userId)
  {
    try
    {
      userService.DeleteUser(User.FindFirst(ClaimTypes.Role)?.Value, User.FindFirst(ClaimTypes.NameIdentifier)?.Value, userId);
      return NoContent();
    }
    catch (KeyNotFoundException knfex)
    {
      return NotFound($"User {userId} not found: {knfex.Message}");
    }
    catch (UnauthorizedAccessException uaex)
    {
      return Unauthorized(uaex.Message);
    }
    catch (Exception ex)
    {
      return BadRequest($" Error deleting user: {ex.Message}");
    }
  }
}