namespace PetPalApp.Business;

public interface IUserService
{
  void RegisterUser(String name, String email, String Password, String UserSupplier);
  void DeleteUser(String key);
  public void DeleteUserService(string userName, string serviceId);
  public void DeleteUserProduct(string userName, string serviceId);
  public int GetIdUser(string name);
  bool checkUserExist(String name, String email);
  bool CheckLogin(string name, string password);
  bool ValidatEmail(String email);
  string ShowAccount(string name);
}
