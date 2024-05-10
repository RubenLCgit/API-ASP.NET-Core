using PetPalApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace PetPalApp.Data;

public class UserEFRepository : IRepositoryGeneric<User>
{
  private readonly PetPalAppContext _context;
  public UserEFRepository(PetPalAppContext context)
  {
    _context = context;
  }

  public void AddEntity(User user)
  {
    try
    {
      _context.Users.Add(user);
      _context.SaveChanges();
    }
    catch (DbUpdateException ex)
    {
      throw new InvalidOperationException("Error adding a new user.", ex);
    }
    
  }
  public void DeleteEntity(User user)
  {
    try
    {
      var userToDelete = _context.Users.Include(u => u.ListServices).Include(u => u.ListProducts).FirstOrDefault(u => u.UserId == user.UserId);
      if (userToDelete == null) throw new KeyNotFoundException("User not found.");
      _context.Users.Remove(userToDelete);
      _context.SaveChanges();
    }
    catch (DbUpdateException ex)
    {
      throw new InvalidOperationException("Error trying to delete the specified user.", ex);
    }
  }
  public List<User> GetAllEntities()
  {
    try
    {
      return _context.Users.Include(u => u.ListServices).Include(u => u.ListProducts).ToList();
    }
    catch (Exception ex)
    {
      throw new InvalidOperationException("Error trying to display all registered users.", ex);
    }
  }
  public User GetByIdEntity(int userId)
  {
    try 
    {
      return _context.Users.Include(u => u.ListServices).Include(u => u.ListProducts).FirstOrDefault(u => u.UserId == userId);
    }
    catch (Exception ex)
    {
      throw new InvalidOperationException($"Error getting a user with id {userId}.", ex);
    }
  }

  public void UpdateEntity(User user) {
    try
    {
      _context.Entry(user).State = EntityState.Modified;
      _context.SaveChanges();
    }
    catch (DbUpdateException ex)
    {
      throw new InvalidOperationException("User update failed.", ex);
    }
  }
  public void SaveChanges()
  {
    _context.SaveChanges();
  }
}