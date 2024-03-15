using PetPalApp.Domain;

namespace PetPalApp.Business;

public interface ISupplierService
{
  void RegisterService(int idUser, String nameUser, String type, String nameService, String description, decimal price, bool online);
  Dictionary<int, Service> SearchService(string serviceType);

  public Dictionary<int, Service> GetAllServices();

  Dictionary<int, Service> ShowMyServices(int idUser);

  public string PrintServices(Dictionary<int, Service> services);

  void DeleteService(int userId, int serviceId);
}