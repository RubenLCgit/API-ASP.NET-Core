namespace PetPalApp.Domain;

public class Product
{ 
  public static int ProductId { get; set; } = 1;
  public int UserId { get; set; }
  public string? ProductName { get; set; }
  public string? ProductDescription { get; set; }
  public decimal ProductPrice { get; set; }
  public DateTime ProductAvailability { get; set; }
  public int ProductStock{ get; set; }
  public double ProductRating { get; set; }
}