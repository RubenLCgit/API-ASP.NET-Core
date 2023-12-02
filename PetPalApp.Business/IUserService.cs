using PetPalApp.Domain;

namespace PetPalApp.Business;

public interface IUserService
{
  void RegisterUser(String name, String email, String Password, String UserSupplier);
  bool checkUserExist(String name, String email);

  bool ValidatEmail(String email);
}
