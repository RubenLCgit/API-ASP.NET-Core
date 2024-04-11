using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetPalApp.Domain;

public class User
{
  [Key]
  public int UserId { get; set; }
  [Required]
  public string UserRole { get; set; }
  [Required]
  public string UserName { get; set; }
  [Required]
  [EmailAddress]
  public string UserEmail { get; set; }
  [Required]
  public String UserPassword { get; set; }
  public DateTime UserRegisterDate { get; set; }
  [Required]
  public bool UserSupplier { get; set; }
  public double UserRating { get; set; }
  public List<Service> ListServices{ get; set; }
  public List<Product> ListProducts{ get; set; }

  public User() { }

  public User(String name, String email, String password, bool supplier)
  {
    UserName = name;
    UserEmail = email;
    UserPassword = password;
    UserSupplier = supplier;
    UserRegisterDate = DateTime.Now;
    UserRating = 0.0;
    ListServices = new List<Service>();
    ListProducts = new List<Product>();
  }
}
