using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetPalApp.Domain;

public class Product
{ 
  [Key]
  public int ProductId { get; set; }
  [ForeignKey("UserId")]
  [Required]
  public int UserId { get; set; }
  [Required]
  public string ProductType{ get; set; }
  [Required]
  public string ProductName { get; set; }
  [Required]
  public string ProductDescription { get; set; }
  [Required]
  public decimal ProductPrice { get; set; }
  public DateTime ProductAvailability { get; set; }
  [Required]
  public bool ProductOnline { get; set; }
  [Required]
  public int ProductStock{ get; set; }
  public double ProductRating { get; set; }
  public string UserEmail { get; set; }


  public Product() { }

  public Product(int userId,String type, String name, String description, decimal price, bool online, int stock, string email)
  {
    UserId = userId;
    ProductType = type;
    ProductName = name;
    ProductDescription = description;
    ProductPrice = price;
    ProductOnline = online;
    ProductStock = stock;
    ProductAvailability = DateTime.Now;
    UserEmail = email;
  }
}