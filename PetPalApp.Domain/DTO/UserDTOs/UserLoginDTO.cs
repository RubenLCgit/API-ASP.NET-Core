using System.ComponentModel.DataAnnotations;

namespace PetPalApp.Domain;

public class UserLoginDTO
{
  [Required]
  [MinLength(3, ErrorMessage = "The name must have at least 3 characters")]
  public string UserName { get; set; }
  [Required]
  [MinLength(7, ErrorMessage = "The password must have at least 7 characters")]
  public string UserPassword { get; set; }

  public UserLoginDTO()
  {
  }
  public UserLoginDTO(User user)
  {
    UserName = user.UserName;
    UserPassword = user.UserPassword;
  }
}