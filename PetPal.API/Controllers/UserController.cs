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
  public ActionResult<Dictionary<string, UserCreateUpdateDTO>> GetAll()
  {
    try
    {
      var users = userService.GetAllUsers();
      return Ok(users);
    }
    catch (Exception ex)
    {
      //logger.LogError(ex, "Error getting all users");
      return BadRequest(ex.Message);
    }
  }

  [HttpGet("{username}")]
  public ActionResult<UserCreateUpdateDTO> Get(string username)
  {
    try
    {
      var user = userService.GetUser(username);
      return Ok(user);
    }
    catch (KeyNotFoundException)
    {
      return NotFound();
    }
    catch (Exception ex)
    {
      return NotFound($"User {username} not found: {ex.Message}");
    }
  }

  [HttpPost]
  public ActionResult<User> Create([FromBody] UserCreateUpdateDTO userCreateUpdateDTO)
  {
    if (!ModelState.IsValid) return BadRequest(ModelState);
    try
    {
      var user = userService.RegisterUser(userCreateUpdateDTO.UserName, userCreateUpdateDTO.UserEmail, userCreateUpdateDTO.UserPassword, userCreateUpdateDTO.UserSupplier);
      return CreatedAtAction(nameof(Get), new { username = user.UserName }, user);
    }
    catch (Exception ex)
    {
      return BadRequest(ex.Message);
    }
  }

  [HttpPut]
  public IActionResult Update(string username ,[FromBody] UserCreateUpdateDTO userCreateUpdateDTO)
  {
    if (!ModelState.IsValid) return BadRequest(ModelState);
    try
    {
      userService.UpdateUser(username, userCreateUpdateDTO);
      return Ok(userCreateUpdateDTO);
    }
    catch (KeyNotFoundException nfex)
    {
      return NotFound($"User {username} not found: {nfex.Message}");
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
  }
}