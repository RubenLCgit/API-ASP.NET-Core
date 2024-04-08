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

  public void UpdateProduct(string tokenRole, string tokenId, int productId, ProductUpdateDTO productUpdateDTO)
  {
    var product = Prepository.GetByIdEntity(productId);
    if (ControlUserAccess.UserHasAccess(tokenRole, tokenId, product.UserId))
    {
      product.ProductType = productUpdateDTO.ProductType;
      product.ProductName = productUpdateDTO.ProductName;
      product.ProductDescription = productUpdateDTO.ProductDescription;
      product.ProductPrice = productUpdateDTO.ProductPrice;
      product.ProductOnline = productUpdateDTO.ProductOnline;
      product.ProductStock = productUpdateDTO.ProductStock;
      Prepository.UpdateEntity(productId, product);
      var user = Urepository.GetByIdEntity(product.UserId);
      user.ListProducts[productId] = product;
      Urepository.UpdateEntity(user.UserId, user);
    }
    else throw new UnauthorizedAccessException("You do not have permissions to modify another user's product.");
  }

  public void DeleteProduct(string tokenRole, string tokenId, int productId)
  {
    var product = Prepository.GetByIdEntity(productId);
    if (ControlUserAccess.UserHasAccess(tokenRole, tokenId, product.UserId))
    {
      User user = Urepository.GetByIdEntity(product.UserId);
      if (user.ListProducts.ContainsKey(productId))
      {
        Prepository.DeleteEntity(product);
        user.ListProducts.Remove(productId);
        Urepository.UpdateEntity(user.UserId, user);
      }
      else throw new KeyNotFoundException("The product you want to delete does not exist or belongs to another user.");
    }
    else throw new UnauthorizedAccessException("You do not have permissions to delete another user's product.");
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

  public Product RegisterProduct(string tokenId, ProductCreateDTO productCreateDTO)
  {
    int userId = int.Parse(tokenId);
    var user = Urepository.GetByIdEntity(userId);
    Product product = new(user.UserId , productCreateDTO.ProductType, productCreateDTO.ProductName, productCreateDTO.ProductDescription, productCreateDTO.ProductPrice, productCreateDTO.ProductOnline, productCreateDTO.ProductStock);
    AssignProductId(product);
    Prepository.AddEntity(product);
    user.ListProducts.Add(product.ProductId, product);
    Urepository.UpdateEntity(userId, user);
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

  public List<ProductDTO> SearchAllProducts(string searchedWord, string sortBy, string sortOrder)
  {
    var query = Prepository.GetAllEntities().AsQueryable();
    if (!string.IsNullOrWhiteSpace(searchedWord))
    {
      query = query.Where(x => x.Value.ProductName.Contains(searchedWord, StringComparison.OrdinalIgnoreCase) || x.Value.ProductDescription.Contains(searchedWord, StringComparison.OrdinalIgnoreCase) || x.Value.ProductType.Contains(searchedWord, StringComparison.OrdinalIgnoreCase));
    }
    switch (sortBy.ToLower())
    {
      case "price":
        if (sortOrder.ToLower() == "asc")
        {
          query = query.OrderBy(x => x.Value.ProductPrice);
        }
        else
        {
          query = query.OrderByDescending(x => x.Value.ProductPrice);
        }
        break;
      case "rating":
        if (sortOrder.ToLower() == "asc")
        {
          query = query.OrderBy(x => x.Value.ProductRating);
        }
        else
        {
          query = query.OrderByDescending(x => x.Value.ProductRating);
        }
        break;
      default:
        throw new ArgumentException("Invalid sort parameter. Valid parameters are 'Price' and 'Rating'.");
    }
    var products = query.Select(x => new ProductDTO(x.Value)).ToList();

    if (products.Count == 0) throw new KeyNotFoundException("No products found.");
    return products;
  }

  public List<ProductDTO> SearchMyProducts(string tokenId, string searchedWord, string sortBy, string sortOrder)
  {
    var query = Urepository.GetByIdEntity(int.Parse(tokenId)).ListProducts.AsQueryable();
    if (!string.IsNullOrWhiteSpace(searchedWord))
    {
      query = query.Where(x => x.Value.ProductName.Contains(searchedWord, StringComparison.OrdinalIgnoreCase) || x.Value.ProductDescription.Contains(searchedWord, StringComparison.OrdinalIgnoreCase) || x.Value.ProductType.Contains(searchedWord, StringComparison.OrdinalIgnoreCase));
    }
    switch (sortBy.ToLower())
    {
      case "date":
        if (sortOrder.ToLower() == "asc")
        {
          query = query.OrderBy(x => x.Value.ProductAvailability);
        }
        else
        {
          query = query.OrderByDescending(x => x.Value.ProductAvailability);
        }
        break;
      case "rating":
        if (sortOrder.ToLower() == "asc")
        {
          query = query.OrderBy(x => x.Value.ProductRating);
        }
        else
        {
          query = query.OrderByDescending(x => x.Value.ProductRating);
        }
        break;
      default:
        throw new ArgumentException("Invalid sort parameter. Valid parameters are 'Date' and 'Rating'.");
    }
    var products = query.Select(x => new ProductDTO(x.Value)).ToList();

    if (products.Count == 0) throw new KeyNotFoundException("No products found.");
    return products;
  }

  public Dictionary<int, Product> ShowMyProducts(int idUser)
  {
    Dictionary<int, Product> userProducts = Urepository.GetByIdEntity(idUser).ListProducts;
    return userProducts;
  }
}