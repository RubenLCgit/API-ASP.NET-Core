using PetPalApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace PetPalApp.Data;

public class ServiceEFRepository : IRepositoryGeneric<Service>
{
  private readonly PetPalAppContext _context;
  public ServiceEFRepository(PetPalAppContext context)
  {
    _context = context;
  }
  public void AddEntity(Service service)
  {
    try
    {
      _context.Services.Add(service);
      _context.SaveChanges();
    }
    catch (DbUpdateException ex)
    {
      throw new InvalidOperationException("Error registering the service.", ex);
    }
    
  }
  public void DeleteEntity(Service service)
  {
    try
    {
      _context.Services.Remove(service);
      _context.SaveChanges();
    }
    catch (DbUpdateException ex)
    {
      throw new InvalidOperationException("Error deleting the requested service.", ex);
    }
  }
  public List<Service> GetAllEntities()
  {
    try
    {
      return _context.Services.ToList();
    }
    catch (Exception ex)
    {
      throw new InvalidOperationException("Error when trying to display all services.", ex);
    }
  }
  public Service GetByIdEntity(int serviceId)
  {
    try 
    {
      return _context.Services.FirstOrDefault(service => service.ServiceId == serviceId);
    }
    catch (Exception ex)
    {
      throw new InvalidOperationException($"Error getting the service with id {serviceId}.", ex);
    }
  }

  public void UpdateEntity(Service service)
  {
    try
    {
      _context.Services.Update(service);
      _context.SaveChanges();
    }
    catch (DbUpdateException ex)
    {
      throw new InvalidOperationException("Service update failed.", ex);
    }
  }

  public void SaveChanges()
  {
    _context.SaveChanges();
  }
}