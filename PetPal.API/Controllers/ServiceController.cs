using PetPalApp.Domain;
using PetPalApp.Business;
using Microsoft.AspNetCore.Mvc;

namespace PetPal.API.Controllers;

[ApiController]
[Route("[controller]")]

public class ServiceController : ControllerBase
{
  private readonly IServiceService serviceService;
  private readonly ILogger<ServiceController> logger;
  public ServiceController(IServiceService _serviceService, ILogger<ServiceController> _logger)
  {
    serviceService = _serviceService;
    logger = _logger;
  }

  [HttpGet]
  public ActionResult<Dictionary<int, ServiceDTO>> GetAll()
  {
    try
    {
      var services = serviceService.GetAllServices();
      return Ok(services);
    }
    catch (KeyNotFoundException knfex)
    {
      return NotFound($"No services found: {knfex.Message}");
    }
    catch (Exception ex)
    {
      return BadRequest($"Error getting all services: {ex.Message}");
    }
  }

  [HttpGet("{serviceId}")]
  public ActionResult<ServiceDTO> Get(int serviceId)
  {
    try
    {
      var service = serviceService.GetService(serviceId);
      return Ok(service);
    }
    catch (KeyNotFoundException knfex)
    {
      return NotFound($"Service {serviceId} not found: {knfex.Message}");
    }
    catch (Exception ex)
    {
      return BadRequest($"Error getting service: {ex.Message}");
    }
  }

  [HttpPost]
  public ActionResult<Service> Create([FromBody] ServiceCreateDTO serviceCreateDTO)
  {
    if (!ModelState.IsValid) return BadRequest(ModelState);
    try
    {
      var service = serviceService.RegisterService(serviceCreateDTO);
      return CreatedAtAction(nameof(Get), new { serviceId = service.ServiceId }, service);
    }
    catch (Exception ex)
    {
      return BadRequest($"Error creating service: {ex.Message}");
    }
  }

  [HttpPut("{serviceId}")]
  public ActionResult<Service> Update(int serviceId, [FromBody] ServiceUpdateDTO serviceUpdateDTO)
  {
    if (!ModelState.IsValid) return BadRequest(ModelState);
    try
    {
      serviceService.UpdateService(serviceId, serviceUpdateDTO);
      return Ok(serviceUpdateDTO);
    }
    catch (KeyNotFoundException knfex)
    {
      return NotFound($"Service {serviceId} not found: {knfex.Message}");
    }
    catch (Exception ex)
    {
      return BadRequest($"Error updating service: {ex.Message}");
    }
  }

  [HttpDelete("{serviceId}")]
  public ActionResult Delete(int serviceId)
  {
    try
    {
      serviceService.DeleteService(serviceId);
      return NoContent();
    }
    catch (KeyNotFoundException knfex)
    {
      return NotFound($"Service {serviceId} not found: {knfex.Message}");
    }
    catch (Exception ex)
    {
      return BadRequest($"Error deleting service: {ex.Message}");
    }
  }
}