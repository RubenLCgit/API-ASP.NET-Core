using PetPalApp.Domain;

namespace PetPalApp.Business;

public interface IServiceService
{
  Service RegisterService(string tokenId, ServiceCreateDTO serviceCreateDTO);
  List<ServiceDTO> SearchAllServices(string searchedWord, string sortBy, string sortOrder);
  List<ServiceDTO> SearchMyServices(string tokenId, string searchedWord, string sortBy, string sortOrder);
  public List<ServiceDTO> GetAllServices();
  void DeleteService(string tokenRole, string tokenId, int serviceId);
  ServiceDTO GetService(int serviceId);
  void UpdateService(string tokenRole, string tokenId, int serviceId, ServiceUpdateDTO serviceUpdateDTO);
}