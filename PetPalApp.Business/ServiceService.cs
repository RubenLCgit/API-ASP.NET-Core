using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using PetPalApp.Data;
using PetPalApp.Domain;

namespace PetPalApp.Business;

public class ServiceService : IServiceService
{
  
  private IRepositoryGeneric<Service> Srepository;
  private IRepositoryGeneric<User> Urepository;

  public ServiceService(IRepositoryGeneric<Service> _srepository, IRepositoryGeneric<User> _urepository)
  {
    Srepository = _srepository;
    Urepository = _urepository;
  }

  public ServiceDTO GetService(int serviceId)
  {
    var service = Srepository.GetByIdEntity(serviceId);
    return new ServiceDTO(service);
  }

  public void UpdateService(int serviceId, ServiceUpdateDTO serviceUpdateDTO)
  {
    var service = Srepository.GetByIdEntity(serviceId);
    service.ServiceType = serviceUpdateDTO.ServiceType;
    service.ServiceName = serviceUpdateDTO.ServiceName;
    service.ServiceDescription = serviceUpdateDTO.ServiceDescription;
    service.ServicePrice = serviceUpdateDTO.ServicePrice;
    service.ServiceOnline = serviceUpdateDTO.ServiceOnline;
    Srepository.UpdateEntity(serviceId, service);
  }

  public Service RegisterService(ServiceCreateDTO serviceCreateDTO)
  {
    var user = Urepository.GetByIdEntity(serviceCreateDTO.UserId);
    Service service = new (user.UserId, serviceCreateDTO.ServiceType, serviceCreateDTO.ServiceName, serviceCreateDTO.ServiceDescription, serviceCreateDTO.ServicePrice, serviceCreateDTO.ServiceOnline);
    AssignServiceId(service);
    Srepository.AddEntity(service);
    user.ListServices.Add(service.ServiceId, service);
    Urepository.UpdateEntity(serviceCreateDTO.UserId, user);
    return service;
  }

  private void AssignServiceId(Service service)
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

  public Dictionary<int, ServiceDTO> GetAllServices()
  {
    if (Srepository.GetAllEntities() == null) throw new KeyNotFoundException("No services found.");
    return ConvertToDictionaryDTO(Srepository.GetAllEntities());
  }

  private Dictionary<int, ServiceDTO> ConvertToDictionaryDTO(Dictionary<int, Service> services)
  {
    return services.ToDictionary(pair => pair.Key, pair => new ServiceDTO(pair.Value));
  }

  public Dictionary<int, Service> ShowMyServices(int idUser)
  {
    Dictionary<int, Service> userServices = Urepository.GetByIdEntity(idUser).ListServices;

    return userServices;
  }

  public void DeleteService(int serviceId)
  {
    var service = Srepository.GetByIdEntity(serviceId);
    User user = Urepository.GetByIdEntity(service.UserId);
    if (user.ListServices.ContainsKey(serviceId))
    {
      Srepository.DeleteEntity(service);
      user.ListServices.Remove(serviceId);
      Urepository.UpdateEntity(user.UserId, user);
    }
    else throw new KeyNotFoundException("The service you want to delete does not exist or belongs to another user.");
  }
}