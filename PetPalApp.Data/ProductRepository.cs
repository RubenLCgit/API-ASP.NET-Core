using System.Text.Json;
using PetPalApp.Domain;

namespace PetPalApp.Data;

public class ProductRepository : IRepositoryGeneric<Product>
{
  public Task AddEntity(Product entity)
  {
    throw new NotImplementedException();
  }

  public Task DeleteEntity(Product entity)
  {
    throw new NotImplementedException();
  }

  public Task<IEnumerable<Product>> GetAllEntities()
  {
    throw new NotImplementedException();
  }

  public Task<Product> GetByIDEntity(int id)
  {
    throw new NotImplementedException();
  }

  public Task SaveChanges()
  {
    throw new NotImplementedException();
  }

  public Task UpdateEntity(Product entity)
  {
    throw new NotImplementedException();
  }
}