namespace PetPalApp.Business;

public interface IUserService
{
  void RegisterUser(String name, String email, String Password, String UserSupplier);
  void DeleteUser(String key);
  public int GetIdUser(string name);
  bool checkUserExist(String name, String email);
  bool CheckLogin(string name, string password);
  bool ValidatEmail(String email);
  string ShowAccount(string name);
}
