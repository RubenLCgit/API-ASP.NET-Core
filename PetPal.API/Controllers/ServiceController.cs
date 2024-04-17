using PetPalApp.Domain;
using PetPalApp.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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
  public ActionResult<List<ServiceDTO>> GetAll()
  {
    try
    {
      logger.LogInformation("Getting all services");
      var services = serviceService.GetAllServices();
      return Ok(services);
    }
    catch (KeyNotFoundException knfex)
    {
      logger.LogWarning($"No services found: {knfex.Message}");
      return NotFound($"No services found: {knfex.Message}");
    }
    catch (Exception ex)
    {
      logger.LogError($"Error getting all services: {ex.Message}");
      return BadRequest($"Error getting all services: {ex.Message}");
    }
  }

  [HttpGet("{serviceId}")]
  public ActionResult<ServiceDTO> Get(int serviceId)
  {
    try
    {
      logger.LogInformation($"Getting service {serviceId}");
      var service = serviceService.GetService(serviceId);
      return Ok(service);
    }
    catch (KeyNotFoundException knfex)
    {
      logger.LogWarning($"Service {serviceId} not found: {knfex.Message}");
      return NotFound($"Service {serviceId} not found: {knfex.Message}");
    }
    catch (Exception ex)
    {
      logger.LogError($"Error getting service: {ex.Message}");
      return BadRequest($"Error getting service: {ex.Message}");
    }
  }

  [HttpGet("SearchAllServices")]
  public ActionResult<List<ServiceDTO>> SearchAllServices(string searchedWord, string sortBy = "price", string sortOrder = "asc")
  {
    try
    {
      logger.LogInformation("Searching all services");
      var services = serviceService.SearchAllServices(searchedWord, sortBy, sortOrder);
      return Ok(services);
    }
    catch (KeyNotFoundException knfex)
    {
      logger.LogWarning($"No services found: {knfex.Message}");
      return NotFound($"No services found: {knfex.Message}");
    }
    catch (ArgumentException aex)
    {
      logger.LogWarning(aex.Message);
      return BadRequest(aex.Message);
    }
    catch (Exception ex)
    {
      logger.LogError($"Error searching services: {ex.Message}");
      return BadRequest($"Error searching services: {ex.Message}");
    }
  }

  [Authorize]
  [HttpGet("SearchMyServices")]
  public ActionResult<List<ServiceDTO>> SearchMyServices(string searchedWord, string sortBy = "Date", string sortOrder = "asc")
  {
    try
    {
      logger.LogInformation("Searching my services");
      var services = serviceService.SearchMyServices(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, searchedWord, sortBy, sortOrder);
      return Ok(services);
    }
    catch (KeyNotFoundException knfex)
    {
      logger.LogWarning($"No services found: {knfex.Message}");
      return NotFound($"No services found: {knfex.Message}");
    }
    catch (ArgumentException aex)
    {
      logger.LogWarning(aex.Message);
      return BadRequest(aex.Message);
    }
    catch (Exception ex)
    {
      logger.LogError($"Error searching services: {ex.Message}");
      return BadRequest($"Error searching services: {ex.Message}");
    }
  }

  [Authorize]
  [HttpPost]
  public ActionResult<Service> Create([FromBody] ServiceCreateDTO serviceCreateDTO)
  {
    if (!ModelState.IsValid) return BadRequest(ModelState);
    try
    {
      logger.LogInformation("Creating service");
      var service = serviceService.RegisterService(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, serviceCreateDTO);
      return CreatedAtAction(nameof(Get), new { serviceId = service.ServiceId }, service);
    }
    catch (Exception ex)
    {
      logger.LogError($"Error creating service: {ex.Message}");
      return BadRequest($"Error creating service: {ex.Message}");
    }
  }

  [Authorize]
  [HttpPut("{serviceId}")]
  public ActionResult<Service> Update(int serviceId, [FromBody] ServiceUpdateDTO serviceUpdateDTO)
  {
    if (!ModelState.IsValid) return BadRequest(ModelState);
    try
    {
      logger.LogInformation($"Updating service {serviceId}");
      serviceService.UpdateService(User.FindFirst(ClaimTypes.Role)?.Value, User.FindFirst(ClaimTypes.NameIdentifier)?.Value, serviceId, serviceUpdateDTO);
      return Ok(serviceUpdateDTO);
    }
    catch (KeyNotFoundException knfex)
    {
      logger.LogWarning($"Service {serviceId} not found: {knfex.Message}");
      return NotFound($"Service {serviceId} not found: {knfex.Message}");
    }
    catch (UnauthorizedAccessException uaex)
    {
      logger.LogWarning(uaex.Message);
      return Unauthorized(uaex.Message);
    }
    catch (Exception ex)
    {
      logger.LogError($"Error updating service: {ex.Message}");
      return BadRequest($"Error updating service: {ex.Message}");
    }
  }

  [Authorize]
  [HttpDelete("{serviceId}")]
  public ActionResult Delete(int serviceId)
  {
    try
    {
      logger.LogInformation($"Deleting service {serviceId}");
      serviceService.DeleteService(User.FindFirst(ClaimTypes.Role)?.Value, User.FindFirst(ClaimTypes.NameIdentifier)?.Value, serviceId);
      return NoContent();
    }
    catch (KeyNotFoundException knfex)
    {
      logger.LogWarning($"Service {serviceId} not found: {knfex.Message}");
      return NotFound($"Service {serviceId} not found: {knfex.Message}");
    }
    catch (UnauthorizedAccessException uaex)
    {
      logger.LogWarning(uaex.Message);
      return Unauthorized(uaex.Message);
    }
    catch (Exception ex)
    {
      logger.LogError($"Error deleting service: {ex.Message}");
      return BadRequest($"Error deleting service: {ex.Message}");
    }
  }
}