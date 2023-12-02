using PetPalApp.Domain;

namespace PetPalApp.Business;

public interface ISupplierService
{
  void RegisterService(int idUser, String type, String name, String description, decimal price, bool online);
  Service SearchService(string serviceType);

  Dictionary<string, Service> ShowAllServices();

  Dictionary<string, Service> ShowMyServices();

  void DeleteService(String userName, string userPassword, int serviceId);
}