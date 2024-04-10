using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetPalApp.Domain;

public class Service
{
  [Key]
  public int ServiceId { get; set; }
  [ForeignKey("UserId")]
  public int UserId { get; set; }
  [Required]
  public string ServiceType{ get; set; }
  [Required]
  public string ServiceName { get; set; }
  [Required]
  public string ServiceDescription { get; set; }
  [Required]
  public decimal ServicePrice { get; set; }
  public DateTime ServiceAvailability { get; set; }
  [Required]
  public bool ServiceOnline { get; set; }
  public double ServiceRating { get; set; }


  public Service() { }

  public Service(int userId ,String type, String name, String description, decimal price, bool online)
  {
    UserId = userId;
    ServiceType = type;
    ServiceName = name;
    ServiceDescription = description;
    ServicePrice = price;
    ServiceOnline = online;
    ServiceAvailability = DateTime.Now;
  }
}