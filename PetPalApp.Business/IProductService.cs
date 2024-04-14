using PetPalApp.Domain;

namespace PetPalApp.Business;

public interface IProductService
{
  Product RegisterProduct(string tokenId, ProductCreateDTO productCreateDTO);
  List<ProductDTO> SearchAllProducts(string searchedWord, string sortBy, string sortOrder);
  List<ProductDTO> SearchMyProducts(string tokenId, string searchedWord, string sortBy, string sortOrder);
  List<ProductDTO> GetAllProducts();
  void DeleteProduct(string tokenRole, string tokenId, int productId);
  ProductDTO GetProduct(int productId);
  void UpdateProduct(string tokenRole, string tokenId, int productId, ProductUpdateDTO productUpdateDTO);
}