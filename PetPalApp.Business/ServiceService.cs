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
      Srepository.UpdateEntity(service);
    }
    else throw new UnauthorizedAccessException("You do not have permissions to modify another user's service.");
  }

  public void DeleteService(string tokenRole, string tokenId, int serviceId)
  {
    var service = Srepository.GetByIdEntity(serviceId);
    if (ControlUserAccess.UserHasAccess(tokenRole, tokenId, service.UserId))
    {
      Srepository.DeleteEntity(service);
    }
    else throw new UnauthorizedAccessException("You do not have permissions to delete another user's service.");
  }

  public List<ServiceDTO> GetAllServices()
  {
    var services = Srepository.GetAllEntities();
    if (services == null) throw new KeyNotFoundException("No services found.");
    return ConvertToListDTO(Srepository.GetAllEntities());
  }

  private List<ServiceDTO> ConvertToListDTO(List<Service> services)
  {
    var serviceDTOs = new List<ServiceDTO>();
    foreach (var service in services)
    {
      serviceDTOs.Add(new ServiceDTO(service));
    }
    return serviceDTOs;
  }

  public Service RegisterService(string tokenId, ServiceCreateDTO serviceCreateDTO)
  {
    int userId = int.Parse(tokenId);
    var user = Urepository.GetByIdEntity(userId);
    Service service = new (userId, serviceCreateDTO.ServiceType, serviceCreateDTO.ServiceName, serviceCreateDTO.ServiceDescription, serviceCreateDTO.ServicePrice, serviceCreateDTO.ServiceOnline, user.UserEmail);
    Srepository.AddEntity(service);
    return service;
  }

  public List<ServiceDTO> SearchAllServices(string searchedWord,string sortBy,string sortOrder)
  {
    var query = Srepository.GetAllEntities().AsQueryable();
    if (!string.IsNullOrWhiteSpace(searchedWord))
    {
      query = query.Where(x => x.ServiceName.Contains(searchedWord, StringComparison.OrdinalIgnoreCase) || x.ServiceDescription.Contains(searchedWord, StringComparison.OrdinalIgnoreCase) || x.ServiceType.Contains(searchedWord, StringComparison.OrdinalIgnoreCase));
    }
    switch (sortBy.ToLower())
    {
      case "price":
        if (sortOrder.ToLower() == "asc")
        {
          query = query.OrderBy(x => x.ServicePrice);
        }
        else
        {
          query = query.OrderByDescending(x => x.ServicePrice);
        }
      break;
      case "rating":
        if (sortOrder.ToLower() == "asc")
        {
          query = query.OrderBy(x => x.ServiceRating);
        }
        else
        {
          query = query.OrderByDescending(x => x.ServiceRating);
        }
      break;
      default:
      throw new ArgumentException("Invalid sort parameter. Valid parameters are 'Price' and 'Rating'."); 
    }
    var services = query.Select(x => new ServiceDTO(x)).ToList();

    if (services.Count == 0) throw new KeyNotFoundException("No services found.");
    return services;
  }

  public List<ServiceDTO> SearchMyServices(string tokenId, string searchedWord, string sortBy, string sortOrder)
  {
    var allServices = Srepository.GetAllEntities().ToList().AsQueryable();
    var query = allServices.Where(x => x.UserId == int.Parse(tokenId));
    if (!string.IsNullOrEmpty(searchedWord))
    {
      query = query.Where(x => x.ServiceName.Contains(searchedWord, StringComparison.OrdinalIgnoreCase) || x.ServiceDescription.Contains(searchedWord, StringComparison.OrdinalIgnoreCase) || x.ServiceType.Contains(searchedWord, StringComparison.OrdinalIgnoreCase));
    }
    switch (sortBy.ToLower())
    {
      case "date":
        if (sortOrder.ToLower() == "asc")
        {
          query = query.OrderBy(x => x.ServiceAvailability);
        }
        else
        {
          query = query.OrderByDescending(x => x.ServiceAvailability);
        }
        break;
      case "rating":
        if (sortOrder.ToLower() == "asc")
        {
          query = query.OrderBy(x => x.ServiceRating);
        }
        else
        {
          query = query.OrderByDescending(x => x.ServiceRating);
        }
        break;
      default:
        throw new ArgumentException("Invalid sort parameter. Valid parameters are 'Date' and 'Rating'.");
    }
    var services = query.Select(x => new ServiceDTO(x)).ToList();

    if (services.Count == 0) throw new KeyNotFoundException("No services found.");
    return services;
  }
}