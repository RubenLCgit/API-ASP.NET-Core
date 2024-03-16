using PetPalApp.Domain;

namespace PetPalApp.Business;

public interface IUserService
{
  User RegisterUser(UserCreateUpdateDTO userCreateUpdateDTO);
  void UpdateUser(int userId, UserCreateUpdateDTO userCreateUpdateDTO);
  void DeleteUser(int userId);
  void DeleteUserService(int userId, int serviceId);
  void DeleteUserProduct(int userId, int productId);
  //int GetIdUser(string userName);
  bool checkUserExist(string userName, string userEmail);
  bool CheckLogin(string userName, string userPassword);
  bool ValidatEmail(string userEmail);
  string ShowAccount(int userId);

  // Nuevos métodos añadidos a la firma de la interfaz para implenetar la funcionalidad de la API
  Dictionary<int, UserDTO> GetAllUsers();
  UserDTO GetUser(int userId);
}
