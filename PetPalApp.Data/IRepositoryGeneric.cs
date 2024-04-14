using PetPalApp.Domain;

namespace PetPalApp.Data;

public interface IRepositoryGeneric<T> where T : class
{
  void AddEntity(T entity);
  T GetByIdEntity(int entityId);
  void UpdateEntity(T entity);
  void DeleteEntity(T entity);
  void SaveChanges();
  List<T> GetAllEntities();
}
