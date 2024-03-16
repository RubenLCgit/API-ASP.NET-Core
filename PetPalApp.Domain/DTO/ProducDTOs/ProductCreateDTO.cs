using System.ComponentModel.DataAnnotations;

namespace PetPalApp.Domain;

public class ProductCreateDTO
{
  [Required]
  [Range(1, int.MaxValue, ErrorMessage = "The user id must be greater than 0")]
  public int UserId { get; set; }
  [Required]
  [MinLength(3, ErrorMessage = "The product type name must be at least 3 characters long")]
  public string ProductType { get; set; }
  [Required]
  [MinLength(3, ErrorMessage = "The name must have at least 3 characters")]
  public string ProductName { get; set; }
  [Required]
  [MaxLength(500, ErrorMessage = "The description must have at most 500 characters")]
  public string ProductDescription { get; set; }
  [Required]
  [Range(0.01, 200, ErrorMessage = "The price must be between 0.01 and 200")]
  public decimal ProductPrice { get; set; }
  [Required]
  public bool ProductOnline { get; set; }
  [Required]
  [Range(1, 1000, ErrorMessage = "The stock must be between 1 and 1000")]
  public int ProductStock { get; set; }


  public ProductCreateDTO()
  {
  }

  public ProductCreateDTO(Product product)
  {
    UserId = product.UserId;
    ProductType = product.ProductType;
    ProductName = product.ProductName;
    ProductDescription = product.ProductDescription;
    ProductPrice = product.ProductPrice;
    ProductOnline = product.ProductOnline;
    ProductStock = product.ProductStock;
  }
}