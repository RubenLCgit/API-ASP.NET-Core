using PetPalApp.Domain;

namespace PetPalApp.Business;

public interface IProductService
{
 void RegisterProduct(int idUser, String nameUser, String type, String nameProduct, String description, decimal price, bool online, int stock);
  Dictionary<string, Product> SearchProduct(string ProductType);

  public Dictionary<string, Product> GetAllProducts();

  Dictionary<string, Product> ShowMyProducts(String key);

  public string PrintProduct(Dictionary<string, Product> product);

  void DeleteProduct(string userName, string productId);
}