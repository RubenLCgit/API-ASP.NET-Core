using PetPalApp.Domain;

namespace PetPalApp.Business;

public interface IServiceService
{
  Service RegisterService(ServiceCreateDTO serviceCreateDTO);
  Dictionary<int, Service> SearchService(string serviceType);

  public Dictionary<int, ServiceDTO> GetAllServices();

  Dictionary<int, Service> ShowMyServices(int idUser);

  public string PrintServices(Dictionary<int, Service> services);

  void DeleteService(int serviceId);

  ServiceDTO GetService(int serviceId);

  void UpdateService(int serviceId, ServiceUpdateDTO serviceUpdateDTO);
}