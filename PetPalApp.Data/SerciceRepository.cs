using System.Text.Json;
using PetPalApp.Domain;

namespace PetPalApp.Data;

public class ServiceRepository : IRepositoryGeneric<Service>
{

  public Dictionary<string, Service> EntityDictionary = new Dictionary<string, Service>();
  private readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ServiceRepository", "ServicesRepository.json");
  public void AddEntity(Service entity)
  {
    Dictionary<String, Service> listServices;
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
    throw new NotImplementedException();
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
  public Service GetByNameEntity(string name)
  {
    throw new NotImplementedException();
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

  public void UpdateEntity(Service entity)
  {
    throw new NotImplementedException();
  }
}