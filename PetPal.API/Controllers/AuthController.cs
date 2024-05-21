using PetPalApp.Domain;
using PetPalApp.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace PetPal.API.Controllers;

[ApiController]
[Route("[controller]")]

public class AuthController : ControllerBase
{
  private readonly IUserService userService;
  private readonly IConfiguration configuration;

  private readonly ILogger<UserController> logger;

  public AuthController(IUserService _userService ,IConfiguration _configuration, ILogger<UserController> _logger)
  {
    userService = _userService;
    configuration = _configuration;
    logger = _logger;
  }

  [HttpPost("login")]
  public IActionResult Login([FromBody] UserLoginDTO userLoginDTO)
  {
    try
    {
      logger.LogInformation("Logging in user");
      var user = userService.CheckLogin(userLoginDTO.UserName, userLoginDTO.UserPassword);
      if (user != null)
      {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(configuration["JwtSettings:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
          Subject = new ClaimsIdentity(new Claim[]
          {
            new Claim(ClaimTypes.Role, user.UserRole),
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
          }),
          Expires = DateTime.UtcNow.AddHours(24),
          SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return Ok(new
        {
          token = tokenHandler.WriteToken(token),
          expiration = tokenDescriptor.Expires
        });
      }
      else
      {
        return Unauthorized("Invalid user name or password");
      }
    }
    catch (Exception ex)
    {
      logger.LogError($"Error logging in: {ex.Message}");
      return BadRequest($"Error logging in: {ex.Message}");
    }
  }
}
