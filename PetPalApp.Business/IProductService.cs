using PetPalApp.Domain;

namespace PetPalApp.Business;

public interface IProductService
{
 void RegisterProduct(int idUser, String nameUser, String type, String nameProduct, String description, decimal price, bool online, int stock);
  Dictionary<int, Product> SearchProduct(string ProductType);

  public Dictionary<int, Product> GetAllProducts();

  Dictionary<int, Product> ShowMyProducts(int idUser);

  public string PrintProduct(Dictionary<int, Product> product);

  void DeleteProduct(int userId, int productId);
}