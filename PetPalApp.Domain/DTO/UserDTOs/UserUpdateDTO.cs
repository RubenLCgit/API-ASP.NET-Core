using System.ComponentModel.DataAnnotations;

namespace PetPalApp.Domain;

public class UserUpdateDTO
{
  [Required]
  [EmailAddress(ErrorMessage = "The email must be a valid email")]
  public string UserEmail { get; set; }
  [Required]
  [MinLength(7, ErrorMessage = "The password must have at least 7 characters")]
  public string UserPassword { get; set; }
  [Required]
  public bool UserSupplier { get; set; }

  public UserUpdateDTO()
  {
  }
  public UserUpdateDTO(User user)
  {
    UserEmail = user.UserEmail;
    UserPassword = user.UserPassword;
    UserSupplier = user.UserSupplier;
  }
}