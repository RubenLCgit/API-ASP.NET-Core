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

  public Dictionary<string, UserDTO> GetAllUsers()
  {
    if (repository.GetAllEntities() == null) throw new KeyNotFoundException("No users found");

    return ConvertToDictionaryDTO(repository.GetAllEntities());
  }

  private Dictionary<string, UserDTO> ConvertToDictionaryDTO(Dictionary<string, User> users)
  {
    return users.ToDictionary(pair => pair.Key, pair => new UserDTO(pair.Value));
  }

  public UserDTO GetUser(string name)
  {
    var user = repository.GetByStringEntity(name);
    if (user == null) throw new KeyNotFoundException("User not found");

    return new UserDTO(user);
  }

  public void UpdateUser(string username, UserUpdateDTO userUpdateDTO)
  {
    if (checkUserExist(name: username))
    {
      var user = repository.GetByStringEntity(username);
      var key = repository.GetKeyByValue(repository.GetAllEntities(), user);
      user.UserName = user.UserName;
      user.UserEmail = userUpdateDTO.UserEmail;
      user.UserPassword = userUpdateDTO.UserPassword;
      user.UserSupplier = userUpdateDTO.UserSupplier;
      repository.UpdateEntity(key, user);
    }
    else throw new KeyNotFoundException("User not found");
  }

  public bool checkUserExist(string name = null, string email = null)
  {
    bool userExist=false;
    var allUsers = repository.GetAllEntities();
    foreach (var item in allUsers)
    {
      if ((name != null && item.Value.UserName.Equals(name, StringComparison.OrdinalIgnoreCase))||(email != null && item.Value.UserEmail.Equals(email, StringComparison.OrdinalIgnoreCase)))
      {
        userExist = true;
        break;
      }
    }
    return userExist;
  }

  public bool CheckLogin(string name, string password)
  {
    bool login=false;
    var allUsers = repository.GetAllEntities();
    foreach (var item in allUsers)
    {
      if (item.Key.Equals(name, StringComparison.OrdinalIgnoreCase)&&item.Value.UserPassword.Equals(password, StringComparison.OrdinalIgnoreCase))
      {
        login = true;
        break;
      }
    }
    return login;
  }

  public User RegisterUser(string name, string email, string password, bool Supplier)
  {
    try
    {
      User user = new(name, email, password, Supplier);
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

  public int GetIdUser(string name)
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
  }

  public string ShowAccount(string name)
  {
    var currentUser = repository.GetByStringEntity(name);
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

  public void DeleteUser(string key)
  {
    var user = repository.GetByStringEntity(key);
    if(user==null)throw new KeyNotFoundException("User not found");
    repository.DeleteEntity(user);
  }

  public void DeleteUserService(string userName, string serviceId )
  {
    var user = repository.GetByStringEntity(userName);
    user.ListServices.Remove(serviceId);
    repository.UpdateEntity(userName, user);
  }

  public void DeleteUserProduct(string userName, string serviceId )
  {
    //var allUsers = repository.GetAllEntities();
    var user = repository.GetByStringEntity(userName);
    user.ListProducts.Remove(serviceId);
    repository.UpdateEntity(userName, user);
  }
}