namespace PetPalApp.Domain;

public class Order
{
  public static int OrderId { get; set; } = 1;
  public DateTime OrderDate { get; set; }
  public int UserId { get; set; }
  public List<OrderDetails> OrderDetails { get; set; }
  public decimal OrderTotalPrice { get; set; }
}