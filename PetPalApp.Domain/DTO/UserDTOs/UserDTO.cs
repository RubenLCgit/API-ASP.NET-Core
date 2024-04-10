namespace PetPalApp.Domain;

public class UserDTO
{
  public int UserId { get; set; }
  public string UserName { get; set; }
  public string UserEmail { get; set; }
  public string UserPassword { get; set; }
  public DateTime UserRegisterDate { get; set; }
  public bool UserSupplier { get; set; }
  public double UserRating { get; set; }

  public List<int> ListIdServices { get; set; }

  public List<int> ListIdProducts { get; set; }


  public UserDTO()
  {
  }
  public UserDTO(User user)
  {
    UserId = user.UserId;
    UserName = user.UserName;
    UserEmail = user.UserEmail;
    UserPassword = user.UserPassword;
    UserRegisterDate = user.UserRegisterDate;
    UserSupplier = user.UserSupplier;
    UserRating = user.UserRating;
    ListIdServices = new List<int>();
    ListIdProducts = new List<int>();

    if (user.ListServices != null)
    {
      foreach (var service in user.ListServices)
      {
        ListIdServices.Add(service.ServiceId);
      }
    }
    if (user.ListProducts != null)
    {
      foreach (var product in user.ListProducts)
      {
        ListIdProducts.Add(product.ProductId);
      }
    }
  }
}