using System.Text.RegularExpressions;
using PetPalApp.Data;
using PetPalApp.Domain;

namespace PetPalApp.Business;

public class SupplierService : ISupplierService
{
  
  private IRepositoryGeneric<Service> repository;

  public SupplierService(IRepositoryGeneric<Service> _repository)
  {
    repository = _repository;
  }

  public void DeleteService(string userName, string userPassword, int serviceId)
  {
    throw new NotImplementedException();
  }

  public void RegisterService(int idUser, string type, string name, string description, decimal price, bool online)
  {
    Service service = new(type, name, description, price, online);
    AssignId(service);
    service.UserId = idUser;
    repository.AddEntity(service);
  }

  private void AssignId(Service service)
  {
    var allUsers = repository.GetAllEntities();
    int nextId = 0;
    int newId;
    if (allUsers == null || allUsers.Count == 0)
    {
      service.ServiceId = "1";
    }
    else
    {
      foreach (var item in allUsers)
      {
        if (int.Parse(item.Value.ServiceId) > nextId)
        {
        nextId = int.Parse(item.Value.ServiceId);
        }
      }
      newId = nextId + 1;
      service.ServiceId = newId.ToString();
    }
  }

  public Service SearchService(string serviceType)
  {
    throw new NotImplementedException();
  }

  public Dictionary<string, Service> ShowAllServices()
  {
    throw new NotImplementedException();
  }

  public Dictionary<string, Service> ShowMyServices()
  {
    throw new NotImplementedException();
  }
}