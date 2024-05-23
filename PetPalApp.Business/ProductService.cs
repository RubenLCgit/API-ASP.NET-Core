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
      Prepository.UpdateEntity(product);
    }
    else throw new UnauthorizedAccessException("You do not have permissions to modify another user's product.");
  }

  public void DeleteProduct(string tokenRole, string tokenId, int productId)
  {
    var product = Prepository.GetByIdEntity(productId);
    if (ControlUserAccess.UserHasAccess(tokenRole, tokenId, product.UserId))
    {
      Prepository.DeleteEntity(product);
    }
    else throw new UnauthorizedAccessException("You do not have permissions to delete another user's product.");
  }

  public List<ProductDTO> GetAllProducts()
  {
    var products = Prepository.GetAllEntities();
    if (products == null) throw new KeyNotFoundException("No products found");
    return ConvertToListDTO(Prepository.GetAllEntities());
  }

  private List<ProductDTO> ConvertToListDTO(List<Product> products)
  {
    List<ProductDTO> productDTOs = new List<ProductDTO>();
    foreach (var product in products)
    {
      productDTOs.Add(new ProductDTO(product));
    }
    return productDTOs;
  }

  public Product RegisterProduct(string tokenId, ProductCreateDTO productCreateDTO)
  {
    int userId = int.Parse(tokenId);
    var user = Urepository.GetByIdEntity(userId);
    Product product = new(userId , productCreateDTO.ProductType, productCreateDTO.ProductName, productCreateDTO.ProductDescription, productCreateDTO.ProductPrice, productCreateDTO.ProductOnline, productCreateDTO.ProductStock, user.UserEmail);
    Prepository.AddEntity(product);
    return product;
  }
  
  public List<ProductDTO> SearchAllProducts(string searchedWord, string sortBy, string sortOrder)
  {
    var query = Prepository.GetAllEntities().AsQueryable();
    if (!string.IsNullOrWhiteSpace(searchedWord))
    {
      query = query.Where(x => x.ProductName.Contains(searchedWord, StringComparison.OrdinalIgnoreCase) || x.ProductDescription.Contains(searchedWord, StringComparison.OrdinalIgnoreCase) || x.ProductType.Contains(searchedWord, StringComparison.OrdinalIgnoreCase));
    }
    switch (sortBy.ToLower())
    {
      case "price":
        if (sortOrder.ToLower() == "asc")
        {
          query = query.OrderBy(x => x.ProductPrice);
        }
        else
        {
          query = query.OrderByDescending(x => x.ProductPrice);
        }
        break;
      case "rating":
        if (sortOrder.ToLower() == "asc")
        {
          query = query.OrderBy(x => x.ProductRating);
        }
        else
        {
          query = query.OrderByDescending(x => x.ProductRating);
        }
        break;
      default:
        throw new ArgumentException("Invalid sort parameter. Valid parameters are 'Price' and 'Rating'.");
    }
    var products = query.Select(x => new ProductDTO(x)).ToList();

    if (products.Count == 0) throw new KeyNotFoundException("No products found.");
    return products;
  }

  public List<ProductDTO> SearchMyProducts(string tokenId, string searchedWord, string sortBy, string sortOrder)
  {
    var allproducts = Prepository.GetAllEntities();
    var query = allproducts.Where(x => x.UserId == int.Parse(tokenId)).ToList().AsQueryable();
    if (!string.IsNullOrWhiteSpace(searchedWord))
    {
      query = query.Where(x => x.ProductName.Contains(searchedWord, StringComparison.OrdinalIgnoreCase) || x.ProductDescription.Contains(searchedWord, StringComparison.OrdinalIgnoreCase) || x.ProductType.Contains(searchedWord, StringComparison.OrdinalIgnoreCase));
    }
    switch (sortBy.ToLower())
    {
      case "date":
        if (sortOrder.ToLower() == "asc")
        {
          query = query.OrderBy(x => x.ProductAvailability);
        }
        else
        {
          query = query.OrderByDescending(x => x.ProductAvailability);
        }
        break;
      case "rating":
        if (sortOrder.ToLower() == "asc")
        {
          query = query.OrderBy(x => x.ProductRating);
        }
        else
        {
          query = query.OrderByDescending(x => x.ProductRating);
        }
        break;
      default:
        throw new ArgumentException("Invalid sort parameter. Valid parameters are 'Date' and 'Rating'.");
    }
    var products = query.Select(x => new ProductDTO(x)).ToList();

    if (products.Count == 0) throw new KeyNotFoundException("No products found.");
    return products;
  }
}