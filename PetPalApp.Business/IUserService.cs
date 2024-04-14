using PetPalApp.Domain;

namespace PetPalApp.Business;

public interface IUserService
{
  User RegisterUser(UserCreateUpdateDTO userCreateUpdateDTO);
  void UpdateUser(string tokenRole, string tokenId, int userId, UserCreateUpdateDTO userCreateUpdateDTO);
  void DeleteUser(string tokenRole, string tokenId, int userId);
  List<UserDTO> GetAllUsers(string userRole);
  UserDTO GetUser(string tokenRole, string tokenId, int userId);
  User CheckLogin(string userName, string userPassword);
}
