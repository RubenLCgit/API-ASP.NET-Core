namespace PetPalApp.Business;

public interface IUserService
{
  Task RegisterUser(String name, String email, String Password, bool UserSupplier);
  Task<bool> LoginUser(String name, String Password);
}
