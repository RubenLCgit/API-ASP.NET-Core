namespace PetPalApp.Domain;

public class Service
{
  public int ServiceId { get; set; }
  public int UserId { get; set; }
  public string ServiceType{ get; set; }
  public string ServiceName { get; set; }
  public string ServiceDescription { get; set; }
  public decimal ServicePrice { get; set; }
  public DateTime ServiceAvailability { get; set; }
  public bool ServiceOnline { get; set; }
  public double ServiceRating { get; set; }


  public Service() { }

  public Service(String type, String name, String description, decimal price, bool online)
  {
    ServiceType = type;
    ServiceName = name;
    ServiceDescription = description;
    ServicePrice = price;
    ServiceOnline = online;
    ServiceAvailability = DateTime.Now;
  }
}