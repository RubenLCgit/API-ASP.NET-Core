namespace PetPalApp.Domain;

public class ServiceDTO
{
  public int ServiceId { get; set; }
  public int UserId { get; set; }
  public string ServiceType { get; set; }
  public string ServiceName { get; set; }
  public string ServiceDescription { get; set; }
  public decimal ServicePrice { get; set; }
  public DateTime ServiceAvailability { get; set; }
  public bool ServiceOnline { get; set; }
  public double ServiceRating { get; set; }
  public string UserEmail { get; set; }

  public ServiceDTO() { }

  public ServiceDTO(Service service)
  {
    ServiceId = service.ServiceId;
    UserId = service.UserId;
    ServiceType = service.ServiceType;
    ServiceName = service.ServiceName;
    ServiceDescription = service.ServiceDescription;
    ServicePrice = service.ServicePrice;
    ServiceAvailability = service.ServiceAvailability;
    ServiceOnline = service.ServiceOnline;
    ServiceRating = service.ServiceRating;
    UserEmail = service.UserEmail;
  }
}