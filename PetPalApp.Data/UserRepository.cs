using System.Text.Json;
using PetPalApp.Domain;

namespace PetPalApp.Data;

public class UserRepository : IRepositoryGeneric<User>
{

  public Dictionary<int, User> EntityDictionary = new Dictionary<int, User>();
  private readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UserRepository", "UsersRepository.json");

  public void AddEntity(User entity)
  {
    Dictionary<int, User> listUsers;
    try
    {
      if (File.Exists(_filePath))
      {
        listUsers = GetAllEntities();
        EntityDictionary = listUsers;
      }
      EntityDictionary.Add(entity.UserId, entity);
      SaveChanges();
    }
    catch (Exception ex)
    {
      throw new Exception("Registration failed", ex);
    }

  }

  public void DeleteEntity(User entity)
  {
    EntityDictionary = GetAllEntities();
    EntityDictionary.Remove(entity.UserId);
    SaveChanges();
  }

  public Dictionary<int, User> GetAllEntities()
  {
    Dictionary<int, User> dictionaryUsers = new Dictionary<int, User>();
    String jsonString;
    if (File.Exists(_filePath))
    {
      jsonString = File.ReadAllText(_filePath);
      dictionaryUsers = JsonSerializer.Deserialize<Dictionary<int, User>>(jsonString);
    }
    else
    {
      dictionaryUsers = EntityDictionary;
    }
    return dictionaryUsers;
  }

  public User GetByIdEntity(int entityId)
  {
    var dictionaryCurrentUser = GetAllEntities();
    User user = null;
    if (dictionaryCurrentUser.ContainsKey(entityId)) user = dictionaryCurrentUser[entityId];
    if (user == null) throw new KeyNotFoundException("User not found");
    return user;
  }

  public void UpdateEntity(int entityId, User user)
  {
    EntityDictionary = GetAllEntities();
    EntityDictionary[entityId] = user;
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