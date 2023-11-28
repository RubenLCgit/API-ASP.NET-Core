using PetPalApp.Domain;

namespace PetPalApp.Data;

public interface IRepositoryGeneric<T> where T : class
{
  Task AddEntity(T entity);
  Task<T> GetByIDEntity(int id);
  Task UpdateEntity(T entity);
  Task DeleteEntity(T entity);
  Task SaveChanges();
  Task<IEnumerable<T>> GetAllEntities();
}
