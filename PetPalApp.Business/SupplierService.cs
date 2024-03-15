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
    var user = Urepository.GetByIdEntity(idUser);
    user.ListServices.Add(service.ServiceId, service);
    Urepository.UpdateEntity(idUser, user);
  }

  private void AssignId(Service service)
  {
    var allServices = Srepository.GetAllEntities();
    int nextId = 0;
    if (allServices == null || allServices.Count == 0)
    {
      service.ServiceId = 1;
    }
    else
    {
      foreach (var item in allServices)
      {
        if (item.Value.ServiceId > nextId)
        {
        nextId = item.Value.ServiceId;
        }
      }
      service.ServiceId = nextId + 1;
    }
  }

  public string PrintServices(Dictionary<int, Service> services)
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

  public Dictionary<int, Service> SearchService(string serviceType)
  {
    var allServices = Srepository.GetAllEntities();
    Dictionary<int, Service> typeServices = new();
    foreach (var item in allServices)
    {
      if (item.Value.ServiceType.IndexOf(serviceType,StringComparison.OrdinalIgnoreCase) >= 0 || item.Value.ServiceDescription.IndexOf(serviceType, StringComparison.OrdinalIgnoreCase) >= 0 || item.Value.ServiceName.IndexOf(serviceType, StringComparison.OrdinalIgnoreCase) >= 0)
      {
        typeServices.Add(item.Value.ServiceId, item.Value);
      }
    }
    return typeServices;
  }

  public Dictionary<int, Service> GetAllServices()
  {
    var allServices = Srepository.GetAllEntities();
    
    return allServices;
  }

  public Dictionary<int, Service> ShowMyServices(int idUser)
  {
    Dictionary<int, Service> userServices = Urepository.GetByIdEntity(idUser).ListServices;

    return userServices;
  }

  public void DeleteService(int userId, int serviceId)
  {
    var service = Srepository.GetByIdEntity(serviceId);
    User user = Urepository.GetByIdEntity(userId);
    if (user.ListServices.ContainsKey(serviceId))
    {
      Srepository.DeleteEntity(service);
    }
    else Console.WriteLine("The service you want to delete does not exist or belongs to another user.");
  }
}