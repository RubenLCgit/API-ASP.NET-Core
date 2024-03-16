using PetPalApp.Domain;

namespace PetPalApp.Business;

public interface IProductService
{
  Product RegisterProduct(ProductCreateDTO productCreateDTO);
  Dictionary<int, Product> SearchProduct(string ProductType);

  public Dictionary<int, ProductDTO> GetAllProducts();

  Dictionary<int, Product> ShowMyProducts(int idUser);

  public string PrintProduct(Dictionary<int, Product> product);

  void DeleteProduct(int productId);

  ProductDTO GetProduct(int productId);

  void UpdateProduct(int productId, ProductUpdateDTO productUpdateDTO);
}