using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using PetPalApp.Data;
using PetPalApp.Domain;

namespace PetPalApp.Business;

public class ProductService : IProductService
{

  private IRepositoryGeneric<Product> Prepository;
  private IRepositoryGeneric<User> Urepository;

  public ProductService(IRepositoryGeneric<Product> _prepository, IRepositoryGeneric<User> _urepository)
  {
    Prepository = _prepository;
    Urepository = _urepository;
  }

  public ProductDTO GetProduct(int productId)
  {
    var product = Prepository.GetByIdEntity(productId);
    return new ProductDTO(product);
  }

  public void UpdateProduct(int productId, ProductUpdateDTO productUpdateDTO)
  {
    var product = Prepository.GetByIdEntity(productId);
    product.ProductType = productUpdateDTO.ProductType;
    product.ProductName = productUpdateDTO.ProductName;
    product.ProductDescription = productUpdateDTO.ProductDescription;
    product.ProductPrice = productUpdateDTO.ProductPrice;
    product.ProductOnline = productUpdateDTO.ProductOnline;
    product.ProductStock = productUpdateDTO.ProductStock;
    Prepository.UpdateEntity(productId, product);
  }

  public void DeleteProduct(int productId)
  {
    var product = Prepository.GetByIdEntity(productId);
    User user = Urepository.GetByIdEntity(product.UserId);
    if (user.ListProducts.ContainsKey(productId))
    {
      Prepository.DeleteEntity(product);
    }
    else throw new KeyNotFoundException("The product you want to delete does not exist or belongs to another user.");
  }

  public Dictionary<int, ProductDTO> GetAllProducts()
  {
    if (Prepository.GetAllEntities() == null) throw new KeyNotFoundException("No products found");

    return ConvertToDictionaryDTO(Prepository.GetAllEntities());
  }

  private Dictionary<int, ProductDTO> ConvertToDictionaryDTO(Dictionary<int, Product> products)
  {
    return products.ToDictionary(pair => pair.Key, pair => new ProductDTO(pair.Value));
  }

  public string PrintProduct(Dictionary<int, Product> products)
  {
    String allDataProducts = "";
    foreach (var item in products)
    {
      string online;
      if (item.Value.ProductOnline) online = "Yes";
      else online = "No";
      String addProduct = @$"

    ====================================================================================
    
    - Type:                         {item.Value.ProductType}
    - Name:                         {item.Value.ProductName}
    - Desciption:                   {item.Value.ProductDescription}
    - Date of availabilility:       {item.Value.ProductAvailability}
    - Home delivery service:        {online}
    - Score:                        {item.Value.ProductRating}
    - Stock:                        {item.Value.ProductStock} units
    - Price:                        {item.Value.ProductPrice} €
    
    ====================================================================================";

      allDataProducts += addProduct;
    }
    return allDataProducts;
  }

  public Product RegisterProduct(ProductCreateDTO productCreateDTO)
  {
    var user = Urepository.GetByIdEntity(productCreateDTO.UserId);
    Product product = new(user.UserId , productCreateDTO.ProductType, productCreateDTO.ProductName, productCreateDTO.ProductDescription, productCreateDTO.ProductPrice, productCreateDTO.ProductOnline, productCreateDTO.ProductStock);
    AssignProductId(product);
    Prepository.AddEntity(product);
    user.ListProducts.Add(product.ProductId, product);
    Urepository.UpdateEntity(productCreateDTO.UserId, user);
    return product;
  }

  private void AssignProductId(Product product)
  {
    var allProducts = Prepository.GetAllEntities();
    int nextId = 0;
    if (allProducts == null || allProducts.Count == 0)
    {
      product.ProductId = 1;
    }
    else
    {
      foreach (var item in allProducts)
      {
        if (item.Value.ProductId > nextId)
        {
        nextId = item.Value.ProductId;
        }
      }
      product.ProductId = nextId + 1;
    }
  }

  public Dictionary<int, Product> SearchProduct(string productType)
  {
    var allProducts = Prepository.GetAllEntities();
    Dictionary<int, Product> typeProducts = new();
    foreach (var item in allProducts)
    {
      if (item.Value.ProductType.IndexOf(productType,StringComparison.OrdinalIgnoreCase) >= 0 || item.Value.ProductDescription.IndexOf(productType, StringComparison.OrdinalIgnoreCase) >= 0 || item.Value.ProductName.IndexOf(productType, StringComparison.OrdinalIgnoreCase) >= 0)
      {
        typeProducts.Add(item.Value.ProductId, item.Value);
      }
    }
    return typeProducts;
  }

  public Dictionary<int, Product> ShowMyProducts(int idUser)
  {
    Dictionary<int, Product> userProducts = Urepository.GetByIdEntity(idUser).ListProducts;
    return userProducts;
  }
}