using PetPalApp.Domain;

namespace PetPalApp.Business;

public interface ISupplierService
{
  void RegisterService(int idUser, String nameUser, String type, String nameService, String description, decimal price, bool online);
  Dictionary<string, Service> SearchService(string serviceType);

  public Dictionary<string, Service> GetAllServices();

  Dictionary<string, Service> ShowMyServices();

  public string PrintServices(Dictionary<string, Service> services);

  void DeleteService(String key);
}