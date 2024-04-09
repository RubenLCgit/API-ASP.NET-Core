using System.Text.RegularExpressions;
using PetPalApp.Data;
using PetPalApp.Domain;
using System.Security.Claims;

namespace PetPalApp.Business;

public class UserService : IUserService
{
  private readonly IRepositoryGeneric<User> repository;
  private readonly IRepositoryGeneric<Product> productRepository;

  private readonly IRepositoryGeneric<Service> serviceRepository;

  public UserService(IRepositoryGeneric<User> _repository, IRepositoryGeneric<Product> _productRepository, IRepositoryGeneric<Service> _serviceRepository)
  {
    repository = _repository;
    productRepository = _productRepository;
    serviceRepository = _serviceRepository;
  }

  public Dictionary<int, UserDTO> GetAllUsers(string userRole)
  {
    if (userRole != "Admin") throw new UnauthorizedAccessException("Utility only available for administrator");
    if (repository.GetAllEntities() == null) throw new KeyNotFoundException("No users found");

    return ConvertToDictionaryDTO(repository.GetAllEntities());
  }

  private Dictionary<int, UserDTO> ConvertToDictionaryDTO(Dictionary<int, User> users)
  {
    return users.ToDictionary(pair => pair.Key, pair => new UserDTO(pair.Value));
  }

  public UserDTO GetUser(string tokenRole , string tokenId, int userId)
  {
    var user = repository.GetByIdEntity(userId);
    if (ControlUserAccess.UserHasAccess(tokenRole, tokenId, userId)) return new UserDTO(user);
    else throw new UnauthorizedAccessException("You do not have permissions to access the requested user's information.");
  }

  public void UpdateUser(string tokenRole, string tokenId, int userId, UserCreateUpdateDTO userCreateUpdateDTO)
  {
    if (ControlUserAccess.UserHasAccess(tokenRole, tokenId, userId))
    {
      var user = repository.GetByIdEntity(userId);
      user.UserName = userCreateUpdateDTO.UserName;
      user.UserEmail = userCreateUpdateDTO.UserEmail;
      user.UserPassword = userCreateUpdateDTO.UserPassword;
      user.UserSupplier = userCreateUpdateDTO.UserSupplier;
      repository.UpdateEntity(userId, user);
    }
    else throw new UnauthorizedAccessException("You do not have permissions to modify another user's data.");
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

  public User CheckLogin(string name, string password)
  {
    User user = null;
    var allUsers = repository.GetAllEntities();
    foreach (var item in allUsers)
    {
      if (item.Value.UserName.Equals(name, StringComparison.OrdinalIgnoreCase) && item.Value.UserPassword.Equals(password, StringComparison.OrdinalIgnoreCase))
      {
        user = item.Value;
        break;
      }
    }
    return user;
  }

  public User RegisterUser(UserCreateUpdateDTO userCreateUpdateDTO)
  {
    try
    {
      User user = new User(userCreateUpdateDTO.UserName, userCreateUpdateDTO.UserEmail, userCreateUpdateDTO.UserPassword, userCreateUpdateDTO.UserSupplier);
      AssignId(user);
      AssignRole(user);
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

  private void AssignRole(User user)
  {
    user.UserRole = user.UserId == 1 ? "Admin" : "Client";
  }

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

  public void DeleteUser(string tokenRole, string tokenId, int userId)
  {
    if (ControlUserAccess.UserHasAccess(tokenRole, tokenId, userId))
    {
      var user = repository.GetByIdEntity(userId);
      if (user.UserRole == "Admin") throw new UnauthorizedAccessException("Cannot delete Administrator account.");
      foreach (var productId in user.ListProducts.Keys.ToList())
      {
        productRepository.DeleteEntity(productRepository.GetByIdEntity(productId));
      }
      foreach (var serviceId in user.ListServices.Keys.ToList())
      {
        serviceRepository.DeleteEntity(serviceRepository.GetByIdEntity(serviceId));
      }
      repository.DeleteEntity(user);
    }
    else throw new UnauthorizedAccessException("You do not have permissions to delete another user's account.");
  }
}