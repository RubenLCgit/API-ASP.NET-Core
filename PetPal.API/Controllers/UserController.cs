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
  public ActionResult<Dictionary<string, UserDTO>> GetAll()
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

  [HttpGet("{username}")]
  public ActionResult<UserDTO> Get(string username)
  {
    try
    {
      var user = userService.GetUser(username);
      return Ok(user);
    }
    catch (KeyNotFoundException knfex)
    {
      return NotFound($"User {username} not found: {knfex.Message}");
    }
    catch (Exception ex)
    {
      return BadRequest($"Error getting user: {ex.Message}");
    }
  }

  [HttpPost]
  public ActionResult<User> Create([FromBody] UserDTO userDTO)
  {
    if (!ModelState.IsValid) return BadRequest(ModelState);
    try
    {
      var user = userService.RegisterUser(userDTO.UserName, userDTO.UserEmail, userDTO.UserPassword, userDTO.UserSupplier);
      return CreatedAtAction(nameof(Get), new { username = user.UserName }, user);
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

  [HttpPut("{username}")]
  public IActionResult Update(string username ,[FromBody] UserUpdateDTO userUpdateDTO)
  {
    if (!ModelState.IsValid) return BadRequest(ModelState);
    try
    {
      userService.UpdateUser(username, userUpdateDTO);
      return Ok(userUpdateDTO);
    }
    catch (KeyNotFoundException nfex)
    {
      return NotFound($"User {username} not found: {nfex.Message}");
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

  [HttpDelete("{username}")]
  public IActionResult Delete(string username)
  {
    try
    {
      userService.DeleteUser(username);
      return NoContent();
    }
    catch (KeyNotFoundException knfex)
    {
      return NotFound($"User {username} not found: {knfex.Message}");
    }
    catch (Exception ex)
    {
      return BadRequest($" Error deleting user: {ex.Message}");
    }
  }
}