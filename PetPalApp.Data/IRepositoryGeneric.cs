using PetPalApp.Domain;

namespace PetPalApp.Data;

public interface IRepositoryGeneric<T> where T : class
{
  void AddEntity(T entity);
  T GetByIDEntity(int id);
  void UpdateEntity(T entity);
  void DeleteEntity(T entity);
  void SaveChanges();
  Dictionary<string, T> GetAllEntities();
}
