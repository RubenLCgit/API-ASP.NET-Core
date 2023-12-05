using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using PetPalApp.Data;
using PetPalApp.Domain;

namespace PetPalApp.Business;

public class SupplierService : ISupplierService
{
  
  private IRepositoryGeneric<Service> Srepository;
  private IRepositoryGeneric<User> Urepository;

  public SupplierService(IRepositoryGeneric<Service> _srepository, IRepositoryGeneric<User> _urepository)
  {
    Srepository = _srepository;
    Urepository = _urepository;
  }

  public void RegisterService(int idUser, String nameUser, String type, String nameService, string description, decimal price, bool online)
  {
    Service service = new(type, nameService, description, price, online);
    AssignId(service);
    service.UserId = idUser;
    Srepository.AddEntity(service);
    var user = Urepository.GetByStringEntity(nameUser);
    user.ListServices.Add(service.ServiceId, service);
    Urepository.UpdateEntity(nameUser, user);
  }

  private void AssignId(Service service)
  {
    var allServices = Srepository.GetAllEntities();
    int nextId = 0;
    int newId;
    if (allServices == null || allServices.Count == 0)
    {
      service.ServiceId = "1";
    }
    else
    {
      foreach (var item in allServices)
      {
        if (int.Parse(item.Value.ServiceId) > nextId)
        {
        nextId = int.Parse(item.Value.ServiceId);
        }
      }
      newId = nextId + 1;
      service.ServiceId = newId.ToString();
    }
  }

  public string PrintServices(Dictionary<string, Service> services)
  {
    String allDataService = "";
    foreach (var item in services)
    {
      string online;
      if (item.Value.ServiceOnline) online = "Yes";
      else online = "No";
      String addService = @$"

    ====================================================================================
    
    - Type:                         {item.Value.ServiceType}
    - Name:                         {item.Value.ServiceName}
    - Desciption:                   {item.Value.ServiceDescription}
    - Date of availabilility:       {item.Value.ServiceAvailability}
    - Home delivery service:        {online}
    - Score:                        {item.Value.ServiceRating}
    - Price:                        {item.Value.ServicePrice} €
    
    ====================================================================================";

      allDataService += addService;
    }
    return allDataService;
  }

  public Dictionary<string, Service> SearchService(string serviceType)
  {
    var allServices = Srepository.GetAllEntities();
    Dictionary<string, Service> typeServices = new();
    foreach (var item in allServices)
    {
      if (item.Value.ServiceType.IndexOf(serviceType,StringComparison.OrdinalIgnoreCase) >= 0 || item.Value.ServiceDescription.IndexOf(serviceType, StringComparison.OrdinalIgnoreCase) >= 0 || item.Value.ServiceName.IndexOf(serviceType, StringComparison.OrdinalIgnoreCase) >= 0)
      {
        typeServices.Add(item.Value.ServiceId, item.Value);
      }
    }
    return typeServices;
  }

  public Dictionary<string, Service> GetAllServices()
  {
    var allServices = Srepository.GetAllEntities();
    
    return allServices;
  }

  public Dictionary<string, Service> ShowMyServices(String key)
  {
    Dictionary<string, Service> userServices = Urepository.GetByStringEntity(key).ListServices;

    return userServices;
  }

  public void DeleteService(string userName, string serviceId)
  {
    var service = Srepository.GetByStringEntity(serviceId);
    User user = Urepository.GetByStringEntity(userName);
    if (user.ListServices.ContainsKey(serviceId))
    {
      Srepository.DeleteEntity(service);
    }
    else Console.WriteLine("The service you want to delete does not exist or belongs to another user.");
  }
}