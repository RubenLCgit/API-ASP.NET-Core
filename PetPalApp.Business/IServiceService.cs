using PetPalApp.Domain;

namespace PetPalApp.Business;

public interface IServiceService
{
  Service RegisterService(string tokenId, ServiceCreateDTO serviceCreateDTO);
  Dictionary<int, Service> SearchService(string serviceType);

  public Dictionary<int, ServiceDTO> GetAllServices();

  Dictionary<int, Service> ShowMyServices(int idUser);

  public string PrintServices(Dictionary<int, Service> services);

  void DeleteService(string tokenRole, string tokenId, int serviceId);

  ServiceDTO GetService(int serviceId);

  void UpdateService(string tokenRole, string tokenId, int serviceId, ServiceUpdateDTO serviceUpdateDTO);
}