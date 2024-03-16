namespace PetPalApp.Domain;

public class Product
{ 
  public int ProductId { get; set; }
  public int UserId { get; set; }
  public string ProductType{ get; set; }
  public string ProductName { get; set; }
  public string ProductDescription { get; set; }
  public decimal ProductPrice { get; set; }
  public DateTime ProductAvailability { get; set; }
  public bool ProductOnline { get; set; }
  public int ProductStock{ get; set; }
  public double ProductRating { get; set; }


  public Product() { }

  public Product(int userId,String type, String name, String description, decimal price, bool online, int stock)
  {
    UserId = userId;
    ProductType = type;
    ProductName = name;
    ProductDescription = description;
    ProductPrice = price;
    ProductOnline = online;
    ProductStock = stock;
    ProductAvailability = DateTime.Now;
  }
}