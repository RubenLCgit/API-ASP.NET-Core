using System.Text.Json;
using PetPalApp.Domain;

namespace PetPalApp.Data;

public class ProductRepository : IRepositoryGeneric<Product>
{
  public void AddEntity(Product entity)
  {
    throw new NotImplementedException();
  }

  public void DeleteEntity(Product entity)
  {
    throw new NotImplementedException();
  }

  public Dictionary<string, Product> GetAllEntities()
  {
    throw new NotImplementedException();
  }

  public Product GetByNameEntity(string name)
  {
    throw new NotImplementedException();
  }

  public void SaveChanges()
  {
    throw new NotImplementedException();
  }

  public void UpdateEntity(Product entity)
  {
    throw new NotImplementedException();
  }
}