using System.Text.Json;
using PetPalApp.Data;
using PetPalApp.Domain;

namespace PetPalApp.Data;

public class UserRepository : IRepositoryGeneric<User>
{

  public Dictionary<string, User> EntityDictionary = new Dictionary<string, User>();

  private readonly string _filePath = "bankAccounts.json";

  public Task AddEntity(User entity)
  {
    throw new NotImplementedException();
  }

  public Task DeleteEntity(User entity)
  {
    throw new NotImplementedException();
  }

  public Task<IEnumerable<User>> GetAllEntities()
  {
    throw new NotImplementedException();
  }

  public Task<User> GetByIDEntity(int id)
  {
    throw new NotImplementedException();
  }

  public Task SaveChanges()
  {
    throw new NotImplementedException();
  }

  public Task UpdateEntity(User entity)
  {
    throw new NotImplementedException();
  }
}