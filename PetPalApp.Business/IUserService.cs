using PetPalApp.Domain;

namespace PetPalApp.Business;

public interface IUserService
{
  User RegisterUser(UserCreateUpdateDTO userCreateUpdateDTO);
  void UpdateUser(string tokenRole, string tokenId, int userId, UserCreateUpdateDTO userCreateUpdateDTO);
  void DeleteUser(string tokenRole, string tokenId, int userId);
  bool checkUserExist(string userName, string userEmail);
  bool ValidatEmail(string userEmail);
  string ShowAccount(int userId);
  Dictionary<int, UserDTO> GetAllUsers(string userRole);
  UserDTO GetUser(string tokenRole, string tokenId, int userId);

  User CheckLogin(string userName, string userPassword);
}
