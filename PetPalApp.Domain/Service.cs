namespace PetPalApp.Domain;

public class Service
{
  public static int ServiceId { get; set; } = 1;
  public int UserId { get; set; }
  public string? ServiceName { get; set; }
  public string? ServiceDescription { get; set; }
  public decimal ServicePrice { get; set; }
  public DateTime ServiceAvailability { get; set; }
  public bool ServiceOnline { get; set; }
  public double ServiceRating { get; set; }
}