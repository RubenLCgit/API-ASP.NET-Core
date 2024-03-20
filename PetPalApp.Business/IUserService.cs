using PetPalApp.Domain;

namespace PetPalApp.Business;

public interface IUserService
{
  User RegisterUser(UserCreateUpdateDTO userCreateUpdateDTO);
  void UpdateUser(int userId, UserCreateUpdateDTO userCreateUpdateDTO);
  void DeleteUser(int userId);
  bool checkUserExist(string userName, string userEmail);
  bool ValidatEmail(string userEmail);
  string ShowAccount(int userId);
  Dictionary<int, UserDTO> GetAllUsers();
  UserDTO GetUser(int userId);

  User CheckLogin(string userName, string userPassword);
}
