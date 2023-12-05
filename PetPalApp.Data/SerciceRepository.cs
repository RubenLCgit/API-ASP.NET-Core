using System.Text.Json;
using PetPalApp.Domain;

namespace PetPalApp.Data;

public class ServiceRepository : IRepositoryGeneric<Service>
{

  public Dictionary<string, Service> EntityDictionary = new Dictionary<string, Service>();
  private readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ServiceRepository", "ServicesRepository.json");
  public void AddEntity(Service entity)
  {
    Dictionary<string, Service> listServices;
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
      throw new Exception("No se ha podido realizar el registro", ex);
    }
  }

  public void DeleteEntity(Service entity)
  {
    EntityDictionary = GetAllEntities();
    EntityDictionary.Remove(entity.ServiceId);
    SaveChanges();
  }

  public Dictionary<string, Service> GetAllEntities()
  {
    Dictionary <string, Service> dictionaryUsers = new Dictionary<string, Service>();;
    String jsonString;
    if (File.Exists(_filePath))
    {
      jsonString = File.ReadAllText(_filePath);
      dictionaryUsers = JsonSerializer.Deserialize<Dictionary<string, Service>>(jsonString);
    }
    else
    {
      dictionaryUsers = EntityDictionary;
    }
    return dictionaryUsers;
  }
  public Service GetByStringEntity(string id)
  {
    var dictionaryCurrentService = GetAllEntities();
    Service service = new();
    foreach (var item in dictionaryCurrentService)
    {
      if (item.Value.ServiceId.Equals(id, StringComparison.OrdinalIgnoreCase))
      {
        service = item.Value;
        break;
      }
    }
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

  public void UpdateEntity(string key, Service service)
  {
    EntityDictionary = GetAllEntities();
    EntityDictionary[key] = service;
    SaveChanges();
  }
}