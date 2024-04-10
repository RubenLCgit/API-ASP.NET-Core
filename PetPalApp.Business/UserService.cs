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

  public List<UserDTO> GetAllUsers(string userRole)
  {
    if (userRole != "Admin") throw new UnauthorizedAccessException("Utility only available for administrator");
    var users = repository.GetAllEntities();
    if (users == null) throw new KeyNotFoundException("No users found");

    return ConvertToListDTO(users);
  }

  private List<UserDTO> ConvertToListDTO(List<User> users)
  {
    List<UserDTO> userDTOs = new List<UserDTO>();
    foreach (var user in users)
    {
      userDTOs.Add(new UserDTO(user));
    }
    return userDTOs;
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
      repository.UpdateEntity(user);
    }
    else throw new UnauthorizedAccessException("You do not have permissions to modify another user's data.");
  }

  public User CheckLogin(string name, string password)
  {
    User user = null;
    var allUsers = repository.GetAllEntities();
    foreach (var currentUser in allUsers)
    {
      if (currentUser.UserName.Equals(name, StringComparison.OrdinalIgnoreCase) && currentUser.UserPassword.Equals(password, StringComparison.OrdinalIgnoreCase))
      {
        user = currentUser;
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
      AssignRole(user);
      repository.AddEntity(user);
      return user;
    }
    catch (Exception ex)
    {
      throw new Exception("Registration could not be completed", ex);
    }
  }

  private void AssignRole(User user)
  {
    user.UserRole = user.UserId == 1 ? "Admin" : "Client";
  }

  public void DeleteUser(string tokenRole, string tokenId, int userId)
  {
    if (ControlUserAccess.UserHasAccess(tokenRole, tokenId, userId))
    {
      var user = repository.GetByIdEntity(userId);
      if (user.UserRole == "Admin") throw new UnauthorizedAccessException("Cannot delete Administrator account.");
      repository.DeleteEntity(user);
    }
    else throw new UnauthorizedAccessException("You do not have permissions to delete another user's account.");
  }
}