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
  public Task<bool> LoginUser(string name, string Password)
  {
    throw new NotImplementedException();
  }

  public Task RegisterUser(string name, string email, string Password, bool UserSupplier)
  {
    throw new NotImplementedException();
  }
}