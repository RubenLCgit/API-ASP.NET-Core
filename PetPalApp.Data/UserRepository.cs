using System.Text.Json;
using PetPalApp.Data;
using PetPalApp.Domain;

namespace PetPalApp.Data;

public class UserRepository : IRepositoryGeneric<User>
{

  public Dictionary<string, User> EntityDictionary = new Dictionary<string, User>();

  private readonly string _filePath = "UsersRepository.json";
  private readonly string _folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"UserRepository","UsersRepository.json");

  public async Task AddEntity(User entity)
  {
    try
    {
      EntityDictionary.Add(entity.UserName, entity);
      await SaveChanges();
    }
    catch (Exception ex)
    {
      throw new Exception("No se ha podido realizar el registro", ex);
    }
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

  public async Task SaveChanges()
  {
    if (!Directory.Exists(_folderPath))
    {
      Directory.CreateDirectory(_folderPath);
    }
    var serializeOptions = new JsonSerializerOptions { WriteIndented = true };
    string jsonString = JsonSerializer.Serialize(EntityDictionary, serializeOptions);
    File.WriteAllText(_filePath, jsonString);
  }

  public Task UpdateEntity(User entity)
  {
    throw new NotImplementedException();
  }
}