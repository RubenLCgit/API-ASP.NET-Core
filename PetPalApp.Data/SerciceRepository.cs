using System.Text.Json;
using PetPalApp.Domain;

namespace PetPalApp.Data;

public class ServiceRepository : IRepositoryGeneric<Service>
{

  public Dictionary<int, Service> EntityDictionary = new Dictionary<int, Service>();
  private readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ServiceRepository", "ServicesRepository.json");
  public void AddEntity(Service entity)
  {
    Dictionary<int, Service> listServices;
    try
    {
      if (File.Exists(_filePath))
      {
        listServices = GetAllEntities();
        EntityDictionary = listServices;
      }
      EntityDictionary.Add(entity.ServiceId, entity);
      SaveChanges();
    }
    catch (Exception ex)
    {
      throw new Exception("Registration failed", ex);
    }
  }

  public void DeleteEntity(Service entity)
  {
    EntityDictionary = GetAllEntities();
    EntityDictionary.Remove(entity.ServiceId);
    SaveChanges();
  }

  public Dictionary<int, Service> GetAllEntities()
  {
    Dictionary <int, Service> dictionaryUsers = new Dictionary<int, Service>();;
    String jsonString;
    if (File.Exists(_filePath))
    {
      jsonString = File.ReadAllText(_filePath);
      dictionaryUsers = JsonSerializer.Deserialize<Dictionary<int, Service>>(jsonString);
    }
    else
    {
      dictionaryUsers = EntityDictionary;
    }
    return dictionaryUsers;
  }
  public Service GetByIdEntity(int entityId)
  {
    var dictionaryCurrentService = GetAllEntities();
    Service service = null;
    if (dictionaryCurrentService.ContainsKey(entityId)) service = dictionaryCurrentService[entityId];
    if (service == null) throw new KeyNotFoundException("User not found");
    return service;
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

  public void UpdateEntity(int entityId, Service service)
  {
    EntityDictionary = GetAllEntities();
    EntityDictionary[entityId] = service;
    SaveChanges();
  }
}