using System.Text.Json;
using PetPalApp.Domain;

namespace PetPalApp.Data;

public class ProductRepository : IRepositoryGeneric<Product>
{
  public Dictionary<int, Product> EntityDictionary = new Dictionary<int, Product>();
  private readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ProductRepository", "ProductRepository.json");
  public void AddEntity(Product entity)
  {
    Dictionary<int, Product> listProducts;
    try
    {
      if (File.Exists(_filePath))
      {
        listProducts = GetAllEntities();
        EntityDictionary = listProducts;
      }
      EntityDictionary.Add(entity.ProductId, entity);
      SaveChanges();
    }
    catch (Exception ex)
    {
      throw new Exception("Registration failed", ex);
    }
  }

  public void DeleteEntity(Product entity)
  {
    EntityDictionary = GetAllEntities();
    EntityDictionary.Remove(entity.ProductId);
    SaveChanges();
  }

  public Dictionary<int, Product> GetAllEntities()
  {
    Dictionary <int, Product> dictionaryProducts = new Dictionary<int, Product>();;
    String jsonString;
    if (File.Exists(_filePath))
    {
     jsonString = File.ReadAllText(_filePath);
     dictionaryProducts = JsonSerializer.Deserialize<Dictionary<int, Product>> (jsonString);
    }
    else
    {
      dictionaryProducts = EntityDictionary;
    }
    return dictionaryProducts;
  }

  public Product GetByIdEntity(int entityId)
  {
    var dictionaryCurrentProduct = GetAllEntities();
    Product product = null;
    if (dictionaryCurrentProduct.ContainsKey(entityId)) product = dictionaryCurrentProduct[entityId];
    if (product == null) throw new KeyNotFoundException("Product not found");
    return product;
  }

  public void SaveChanges()
  {
    string directoryPath = Path.GetDirectoryName(_filePath);
    if (!Directory.Exists(directoryPath))
    {
      Directory.CreateDirectory(directoryPath);
    }
    var serializeOptions = new JsonSerializerOptions { WriteIndented = true };
    string jsonString = JsonSerializer.Serialize(EntityDictionary, serializeOptions);
    File.WriteAllText(_filePath, jsonString);
  }

  public void UpdateEntity(int entityId, Product product)
  {
    EntityDictionary = GetAllEntities();
    EntityDictionary[entityId] = product;
    SaveChanges();
  }
}