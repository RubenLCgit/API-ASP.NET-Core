using PetPalApp.Domain;
using PetPalApp.Business;
using Microsoft.AspNetCore.Mvc;

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


  [HttpGet]
  public ActionResult<Dictionary<int, UserDTO>> GetAll()
  {
    try
    {
      var users = userService.GetAllUsers();
      return Ok(users);
    }
    catch (KeyNotFoundException knfex)
    {
      return NotFound($"No users found: {knfex.Message}");
    }
    catch (Exception ex)
    {
      return BadRequest($"Error getting all users: {ex.Message}");
    }
  }

  [HttpGet("{userId}")]
  public ActionResult<UserDTO> Get(int userId)
  {
    try
    {
      var user = userService.GetUser(userId);
      return Ok(user);
    }
    catch (KeyNotFoundException knfex)
    {
      return NotFound($"User {userId} not found: {knfex.Message}");
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

  [HttpPut("{userId}")]
  public IActionResult Update(int userId, [FromBody] UserCreateUpdateDTO userCreateUpdateDTO)
  {
    if (!ModelState.IsValid) return BadRequest(ModelState);
    try
    {
      userService.UpdateUser(userId, userCreateUpdateDTO);
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
    catch (Exception ex)
    {
      return BadRequest($" Error updating user data: {ex.Message}");
    }
  }

  [HttpDelete("{userId}")]
  public IActionResult Delete(int userId)
  {
    try
    {
      userService.DeleteUser(userId);
      return NoContent();
    }
    catch (KeyNotFoundException knfex)
    {
      return NotFound($"User {userId} not found: {knfex.Message}");
    }
    catch (Exception ex)
    {
      return BadRequest($" Error deleting user: {ex.Message}");
    }
  }
}