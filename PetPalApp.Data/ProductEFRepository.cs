using PetPalApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace PetPalApp.Data;

public class ProductEFRepository : IRepositoryGeneric<Product>
{
  private readonly PetPalAppContext _context;
  public ProductEFRepository(PetPalAppContext context)
  {
    _context = context;
  }
  public void AddEntity(Product product)
  {
    try
    {
      _context.Products.Add(product);
      _context.SaveChanges();
    }
    catch (DbUpdateException ex)
    {
      throw new InvalidOperationException("Error registering the product.", ex);
    }
    
  }
  public void DeleteEntity(Product product)
  {
    try
    {
      _context.Products.Remove(product);
      _context.SaveChanges();
    }
    catch (DbUpdateException ex)
    {
      throw new InvalidOperationException("Error deleting the requested product.", ex);
    }
  }
  public List<Product> GetAllEntities()
  {
    try
    {
      return _context.Products.ToList();
    }
    catch (Exception ex)
    {
      throw new InvalidOperationException("Error when trying to display all products.", ex);
    }
  }
  public Product GetByIdEntity(int productId)
  {
    try 
    {
      return _context.Products.FirstOrDefault(product => product.ProductId == productId);
    }
    catch (Exception ex)
    {
      throw new InvalidOperationException($"Error getting the product with id {productId}.", ex);
    }
  }

  public void UpdateEntity(Product product)
  {
    try
    {
      _context.Products.Update(product);
      _context.SaveChanges();
    }
    catch (DbUpdateException ex)
    {
      throw new InvalidOperationException("Error updating the product.", ex);
    }
  }
  public void SaveChanges()
  {
    _context.SaveChanges();
  }
}