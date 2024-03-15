using PetPalApp.Domain;

namespace PetPalApp.Data;

public interface IRepositoryGeneric<T> where T : class
{
  void AddEntity(T entity);
  T GetByIdEntity(int entityId);
  void UpdateEntity(int entityId, T entity);
  void DeleteEntity(T entity);
  void SaveChanges();
  Dictionary<int, T> GetAllEntities();
}
