using System.Text.Json;
using PetPalApp.Domain;

namespace PetPalApp.Data;

public class ServiceRepository : IRepositoryGeneric<Service>
{
  public Task AddEntity(Service entity)
  {
    throw new NotImplementedException();
  }

  public Task DeleteEntity(Service entity)
  {
    throw new NotImplementedException();
  }

  public Task<IEnumerable<Service>> GetAllEntities()
  {
    throw new NotImplementedException();
  }

  public Task<Service> GetByIDEntity(int id)
  {
    throw new NotImplementedException();
  }

  public Task SaveChanges()
  {
    throw new NotImplementedException();
  }

  public Task UpdateEntity(Service entity)
  {
    throw new NotImplementedException();
  }
}