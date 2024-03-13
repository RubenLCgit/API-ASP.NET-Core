using PetPalApp.Domain;

namespace PetPalApp.Business;

public interface IUserService
{
  User RegisterUser(String name, String email, String Password, bool Supplier);
  void UpdateUser(string key, UserUpdateDTO userUpdateDTO);
  void DeleteUser(String key);
  void DeleteUserService(string userName, string serviceId);
  void DeleteUserProduct(string userName, string serviceId);
  int GetIdUser(string name);
  bool checkUserExist(String name, String email);
  bool CheckLogin(string name, string password);
  bool ValidatEmail(String email);
  string ShowAccount(string name);

  // Nuevos métodos añadidos a la firma de la interfaz para implenetar la funcionalidad de la API
  Dictionary<string, UserDTO> GetAllUsers();
  UserDTO GetUser(string name);
}
