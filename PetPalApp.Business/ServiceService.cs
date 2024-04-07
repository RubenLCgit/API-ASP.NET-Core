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

  public void UpdateService(string tokenRole, string tokenId, int serviceId, ServiceUpdateDTO serviceUpdateDTO)
  {
    var service = Srepository.GetByIdEntity(serviceId);
    if (ControlUserAccess.UserHasAccess(tokenRole, tokenId, service.UserId))
    {
      service.ServiceType = serviceUpdateDTO.ServiceType;
      service.ServiceName = serviceUpdateDTO.ServiceName;
      service.ServiceDescription = serviceUpdateDTO.ServiceDescription;
      service.ServicePrice = serviceUpdateDTO.ServicePrice;
      service.ServiceOnline = serviceUpdateDTO.ServiceOnline;
      Srepository.UpdateEntity(serviceId, service);
      var user = Urepository.GetByIdEntity(service.UserId);
      user.ListServices[serviceId] = service;
      Urepository.UpdateEntity(user.UserId, user);
    }
    else throw new UnauthorizedAccessException("You do not have permissions to modify another user's service.");
  }

  public Service RegisterService(string tokenId, ServiceCreateDTO serviceCreateDTO)
  {
    int userId = int.Parse(tokenId);
    var user = Urepository.GetByIdEntity(userId);
    Service service = new (user.UserId, serviceCreateDTO.ServiceType, serviceCreateDTO.ServiceName, serviceCreateDTO.ServiceDescription, serviceCreateDTO.ServicePrice, serviceCreateDTO.ServiceOnline);
    AssignServiceId(service);
    Srepository.AddEntity(service);
    user.ListServices.Add(service.ServiceId, service);
    Urepository.UpdateEntity(userId, user);
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

  public List<ServiceDTO> SearchService(string searchedWord,string sortBy,string sortOrder)
  {
    var query = Srepository.GetAllEntities().AsQueryable();
    if (!string.IsNullOrWhiteSpace(searchedWord))
    {
      query = query.Where(x => x.Value.ServiceName.Contains(searchedWord, StringComparison.OrdinalIgnoreCase) || x.Value.ServiceDescription.Contains(searchedWord, StringComparison.OrdinalIgnoreCase) || x.Value.ServiceType.Contains(searchedWord, StringComparison.OrdinalIgnoreCase));
    }
    switch (sortBy.ToLower())
    {
      case "price":
        if (sortOrder.ToLower() == "asc")
        {
          query = query.OrderBy(x => x.Value.ServicePrice);
        }
        else
        {
          query = query.OrderByDescending(x => x.Value.ServicePrice);
        }
      break;
      case "rating":
        if (sortOrder.ToLower() == "asc")
        {
          query = query.OrderBy(x => x.Value.ServiceRating);
        }
        else
        {
          query = query.OrderByDescending(x => x.Value.ServiceRating);
        }
      break;
      default:
      throw new ArgumentException("Invalid sort parameter. Valid parameters are 'Price' and 'Rating'."); 
    }
    var services = query.Select(x => new ServiceDTO(x.Value)).ToList();

    if (services.Count == 0) throw new KeyNotFoundException("No services found.");
    return services;
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

  public void DeleteService(string tokenRole, string tokenId, int serviceId)
  {
    var service = Srepository.GetByIdEntity(serviceId);
    if (ControlUserAccess.UserHasAccess(tokenRole, tokenId, service.UserId))
    {
      User user = Urepository.GetByIdEntity(service.UserId);
      if (user.ListServices.ContainsKey(serviceId))
      {
        Srepository.DeleteEntity(service);
        user.ListServices.Remove(serviceId);
        Urepository.UpdateEntity(user.UserId, user);
      }
      else throw new KeyNotFoundException("The service you want to delete does not exist or belongs to another user.");
    }
    else throw new UnauthorizedAccessException("You do not have permissions to delete another user's service.");
  }
}