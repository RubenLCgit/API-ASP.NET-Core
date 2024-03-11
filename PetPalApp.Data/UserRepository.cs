using System.Text.Json;
using PetPalApp.Domain;

namespace PetPalApp.Data;

public class UserRepository : IRepositoryGeneric<User>
{

  public Dictionary<string, User> EntityDictionary = new Dictionary<string, User>();
  private readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UserRepository", "UsersRepository.json");

  public void AddEntity(User entity)
  {
    Dictionary<String, User> listUsers;
    
    if (File.Exists(_filePath))
    {
      listUsers = GetAllEntities();
      EntityDictionary = listUsers;
    }
    EntityDictionary.Add(entity.UserName, entity);
    SaveChanges();
  }

  public void DeleteEntity(User entity)
  {
    EntityDictionary = GetAllEntities();
    EntityDictionary.Remove(entity.UserName);
    SaveChanges();
  }

  public Dictionary<string, User> GetAllEntities()
  {
    Dictionary <String, User> dictionaryUsers = new Dictionary<string, User>();;
    String jsonString;
    if (File.Exists(_filePath))
    {
      jsonString = File.ReadAllText(_filePath);
      dictionaryUsers = JsonSerializer.Deserialize<Dictionary<string, User>>(jsonString);
    }
    else
    {
      dictionaryUsers = EntityDictionary;
    }
    return dictionaryUsers;
  }

  public User GetByStringEntity(string name)
  {
    var dictionaryCurrentUser = GetAllEntities();
    User user = null;
    foreach (var item in dictionaryCurrentUser)
    {
      if (item.Value.UserName.Equals(name, StringComparison.OrdinalIgnoreCase))
      {
        user = item.Value;
        break;
      }
    }
    return user;
  }

  public void UpdateEntity(String key, User user)
  {
    EntityDictionary = GetAllEntities();
    EntityDictionary[key] = user;
    SaveChanges();
  }

  public void SaveChanges()
  {
    string directoryPath = Path.GetDirectoryName(_filePath);
    if (!Directory.Exists(directoryPath))
    {
      Directory.CreateDirectory(directoryPath);
    }
    var serializeOptions = new JsonSerializerOptions { WriteIndented = true };
    string jsonString = JsonSerializer.Serialize(EntityDictionary, serializeOptions);
    File.WriteAllText(_filePath, jsonString);
  }
}