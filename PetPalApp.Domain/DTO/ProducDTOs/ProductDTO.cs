namespace PetPalApp.Domain;

public class ProductDTO
{
  public int ProductId { get; set; }
  public int UserId { get; set; }
  public string ProductType { get; set; }
  public string ProductName { get; set; }
  public string ProductDescription { get; set; }
  public decimal ProductPrice { get; set; }
  public DateTime ProductAvailability { get; set; }
  public bool ProductOnline { get; set; }
  public int ProductStock { get; set; }
  public double ProductRating { get; set; }
  public string UserEmail { get; set; }


  public ProductDTO() { }

  public ProductDTO(Product product)
  {
    ProductId = product.ProductId;
    UserId = product.UserId;
    ProductType = product.ProductType;
    ProductName = product.ProductName;
    ProductDescription = product.ProductDescription;
    ProductPrice = product.ProductPrice;
    ProductAvailability = product.ProductAvailability;
    ProductOnline = product.ProductOnline;
    ProductStock = product.ProductStock;
    ProductRating = product.ProductRating;
    UserEmail = product.UserEmail;
  }
}