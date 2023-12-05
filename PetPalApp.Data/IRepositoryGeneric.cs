using PetPalApp.Domain;

namespace PetPalApp.Data;

public interface IRepositoryGeneric<T> where T : class
{
  void AddEntity(T entity);
  T GetByStringEntity(string key);
  void UpdateEntity(string key, T entity);
  void DeleteEntity(T entity);
  void SaveChanges();
  Dictionary<string, T> GetAllEntities();
}
