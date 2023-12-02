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

  public bool checkUserExist(string name, string email)
  {
    bool userExist=false;
    var allUsers = repository.GetAllEntities();
    foreach (var item in allUsers)
    {
      if (item.Key.Equals(name, StringComparison.OrdinalIgnoreCase)||item.Value.UserEmail.Equals(email, StringComparison.OrdinalIgnoreCase))
      {
        userExist = true;
        break;
      }
    }
    return userExist;
  }

  public void RegisterUser(string name, string email, string password, String stringSupplier)
  {
    bool boolSupplier;
    if (String.Equals(stringSupplier, "Y", StringComparison.OrdinalIgnoreCase))
    {
      boolSupplier = true;
    }
    else
    {
      boolSupplier = false;
    }
    User user = new(name, email, password, boolSupplier);
    repository.AddEntity(user);
  }

  public bool ValidatEmail(string email)
  {
    string regularExpresionEmail = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
    return Regex.IsMatch(email, regularExpresionEmail);
  }
}