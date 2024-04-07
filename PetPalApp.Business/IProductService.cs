using PetPalApp.Domain;

namespace PetPalApp.Business;

public interface IProductService
{
  Product RegisterProduct(string tokenId, ProductCreateDTO productCreateDTO);
  Dictionary<int, Product> SearchProduct(string ProductType);

  public Dictionary<int, ProductDTO> GetAllProducts();

  Dictionary<int, Product> ShowMyProducts(int idUser);

  public string PrintProduct(Dictionary<int, Product> product);

  void DeleteProduct(string tokenRole, string tokenId, int productId);

  ProductDTO GetProduct(int productId);

  void UpdateProduct(string tokenRole, string tokenId, int productId, ProductUpdateDTO productUpdateDTO);
}