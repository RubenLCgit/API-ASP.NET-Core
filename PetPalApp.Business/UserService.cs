using System.Text.RegularExpressions;
using PetPalApp.Data;
using PetPalApp.Domain;

namespace PetPalApp.Business;

public class UserService : IUserService
{
  private readonly IRepositoryGeneric<User> repository;

  public UserService(IRepositoryGeneric<User> _repository)
  {
    repository = _repository;
  }

  public Dictionary<int, UserDTO> GetAllUsers()
  {
    if (repository.GetAllEntities() == null) throw new KeyNotFoundException("No users found");

    return ConvertToDictionaryDTO(repository.GetAllEntities());
  }

  private Dictionary<int, UserDTO> ConvertToDictionaryDTO(Dictionary<int, User> users)
  {
    return users.ToDictionary(pair => pair.Key, pair => new UserDTO(pair.Value));
  }

  public UserDTO GetUser(int userId)
  {
    var user = repository.GetByIdEntity(userId);
    return new UserDTO(user);
  }

  public void UpdateUser(int userId, UserCreateUpdateDTO userCreateUpdateDTO)
  {
    var user = repository.GetByIdEntity(userId);
    user.UserName = userCreateUpdateDTO.UserName;
    user.UserEmail = userCreateUpdateDTO.UserEmail;
    user.UserPassword = userCreateUpdateDTO.UserPassword;
    user.UserSupplier = userCreateUpdateDTO.UserSupplier;
    repository.UpdateEntity(userId, user);
  }

  public bool checkUserExist(string name = null, string email = null)
  {
    bool userExist = false;
    var allUsers = repository.GetAllEntities();
    foreach (var item in allUsers)
    {
      if ((name != null && item.Value.UserName.Equals(name, StringComparison.OrdinalIgnoreCase)) || (email != null && item.Value.UserEmail.Equals(email, StringComparison.OrdinalIgnoreCase)))
      {
        userExist = true;
        break;
      }
    }
    return userExist;
  }

  public bool CheckLogin(string name, string password)
  {
    bool login = false;
    var allUsers = repository.GetAllEntities();
    foreach (var item in allUsers)
    {
      if (item.Value.UserName.Equals(name, StringComparison.OrdinalIgnoreCase) && item.Value.UserPassword.Equals(password, StringComparison.OrdinalIgnoreCase))
      {
        login = true;
        break;
      }
    }
    return login;
  }

  public User RegisterUser(UserCreateUpdateDTO userCreateUpdateDTO)
  {
    try
    {
      User user = new User(userCreateUpdateDTO.UserName, userCreateUpdateDTO.UserEmail, userCreateUpdateDTO.UserPassword, userCreateUpdateDTO.UserSupplier);
      AssignId(user);
      repository.AddEntity(user);
      return user;
    }
    catch (Exception ex)
    {
      throw new Exception("Registration could not be completed", ex);
    }
  }

  private void AssignId(User user)
  {
    var allUsers = repository.GetAllEntities();
    int nextId = 0;
    if (allUsers == null || allUsers.Count == 0)
    {
      user.UserId = 1;
    }
    else
    {
      foreach (var item in allUsers)
      {
        if (item.Value.UserId > nextId)
        {
          nextId = item.Value.UserId;
        }
      }
      user.UserId = nextId + 1;
    }
  }

  /*public int GetIdUser(string name)
  {
    var allUsers = repository.GetAllEntities();
    foreach (var item in allUsers)
    {
      if (item.Key.Equals(name, StringComparison.OrdinalIgnoreCase))
      {
        return item.Value.UserId;
      }
    }
    return 0;
  }*/

  public string ShowAccount(int userId)
  {
    var currentUser = repository.GetByIdEntity(userId);
    String supplier;
    if (currentUser.UserSupplier == true) supplier = "Yes, I am a supplier";
    else supplier = "No, I am not a supplier";
    String userData = @$"
    ================================================================
    
          - ID:                 {currentUser.UserId}
          - Name:               {currentUser.UserName}
          - Email:              {currentUser.UserEmail}
          - Password:           {currentUser.UserPassword}
          - Register date:      {currentUser.UserRegisterDate}
          - Supplier:           {supplier}
          - Score:              {currentUser.UserRating}
    
    ================================================================";

    return userData;
  }

  public bool ValidatEmail(string email)
  {
    string regularExpresionEmail = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
    return Regex.IsMatch(email, regularExpresionEmail);
  }

  public void DeleteUser(int userId)
  {
    var user = repository.GetByIdEntity(userId);
    repository.DeleteEntity(user);
  }

  public void DeleteUserService(int userId, int serviceId)
  {
    var user = repository.GetByIdEntity(userId);
    user.ListServices.Remove(serviceId);
    repository.UpdateEntity(userId, user);
  }

  public void DeleteUserProduct(int userId, int productId)
  {
    //var allUsers = repository.GetAllEntities();
    var user = repository.GetByIdEntity(userId);
    user.ListProducts.Remove(productId);
    repository.UpdateEntity(userId, user);
  }
}