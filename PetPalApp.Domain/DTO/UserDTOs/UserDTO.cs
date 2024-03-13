using System.ComponentModel.DataAnnotations;

namespace PetPalApp.Domain;

public class UserDTO
{
  [Required]
  [MinLength(3, ErrorMessage = "The user name must have at least 3 characters")]
  public string UserName { get; set; }
  [Required]
  [EmailAddress(ErrorMessage = "The email must be a valid email")]
  public string UserEmail { get; set; }
  [Required]
  [MinLength(7, ErrorMessage = "The password must have at least 7 characters")]
  public string UserPassword { get; set; }
  [Required]
  public bool UserSupplier { get; set; }

  
  public UserDTO()
  {
  }
  public UserDTO(User user)
  {
    UserName = user.UserName;
    UserEmail = user.UserEmail;
    UserPassword = user.UserPassword;
    UserSupplier = user.UserSupplier;
  }
}