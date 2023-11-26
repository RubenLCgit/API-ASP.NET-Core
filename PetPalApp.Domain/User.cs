namespace PetPalApp.Domain;

public class User
{
  public static int UserId { get; set; } = 1;
  public string? UserName { get; set; }
  public string? UserEmail { get; set; }
  public String? UserPassword { get; set; }
  public DateTime UserRegisterDate { get; set; }
  public bool UserSupplier { get; set; }
  public double UserRating { get; set; }

}
