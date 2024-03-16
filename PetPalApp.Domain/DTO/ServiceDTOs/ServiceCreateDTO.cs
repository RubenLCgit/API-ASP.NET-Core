using System.ComponentModel.DataAnnotations;

namespace PetPalApp.Domain;

public class ServiceCreateDTO
{
  [Required]
  [Range(1, int.MaxValue, ErrorMessage = "The user id must be greater than 0")]
  public int UserId { get; set; }
  [Required]
  [MinLength(3, ErrorMessage = "The service type name must be at least 3 characters long")]
  public string ServiceType { get; set; }
  [Required]
  [MinLength(3, ErrorMessage = "The name must have at least 3 characters")]
  public string ServiceName { get; set; }
  [Required]
  [MaxLength(500, ErrorMessage = "The description must have at most 500 characters")]
  public string ServiceDescription { get; set; }
  [Required]
  [Range(0.01, 200, ErrorMessage = "The price must be between 0.01 and 200")]
  public decimal ServicePrice { get; set; }
  [Required]
  public bool ServiceOnline { get; set; }
  
  public ServiceCreateDTO()
  {
  }

  public ServiceCreateDTO(Service service)
  {
    UserId = service.UserId;
    ServiceType = service.ServiceType;
    ServiceName = service.ServiceName;
    ServiceDescription = service.ServiceDescription;
    ServicePrice = service.ServicePrice;
    ServiceOnline = service.ServiceOnline;
  }
}