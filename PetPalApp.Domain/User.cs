namespace PetPalApp.Domain;

public class User
{
  public int UserId { get; set; }
  public string UserName { get; set; }
  public string UserEmail { get; set; }
  public String UserPassword { get; set; }
  public DateTime UserRegisterDate { get; set; }
  public bool UserSupplier { get; set; }
  public double UserRating { get; set; }

  private static int UserIdSeed = 1;

  public User() { }

  public User(String name, String email, String password, bool supplier)
  {
    UserName = name;
    UserEmail = email;
    UserPassword = password;
    UserSupplier = supplier;
    UserId = UserIdSeed;
    UserIdSeed++;
    UserRegisterDate = DateTime.Now;
    UserRating = 0.0;
  }
}
