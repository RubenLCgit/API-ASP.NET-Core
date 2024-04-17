using PetPalApp.Domain;
using PetPalApp.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace PetPal.API.Controllers;


[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
  private readonly IProductService productService;
  private readonly ILogger<ProductController> logger;
  public ProductController(IProductService _productService, ILogger<ProductController> _logger)
  {
    productService = _productService;
    logger = _logger;
  }


  [HttpGet]
  public ActionResult<List<ProductDTO>> GetAll()
  {
    try
    {
      logger.LogInformation("Getting all products");
      var products = productService.GetAllProducts();
      return Ok(products);
    }
    catch (KeyNotFoundException knfex)
    {
      logger.LogWarning($"No products found: {knfex.Message}");
      return NotFound($"No products found: {knfex.Message}");
    }
    catch (Exception ex)
    {
      logger.LogError($"Error getting all products: {ex.Message}");
      return BadRequest($"Error getting all products: {ex.Message}");
    }
  }

  [HttpGet("{productId}")]
  public ActionResult<ProductDTO> Get(int productId)
  {
    try
    {
      logger.LogInformation($"Getting product {productId}");
      var product = productService.GetProduct(productId);
      return Ok(product);
    }
    catch (KeyNotFoundException knfex)
    {
      logger.LogWarning($"Product {productId} not found: {knfex.Message}");
      return NotFound($"Product {productId} not found: {knfex.Message}");
    }
    catch (Exception ex)
    {
      logger.LogError($"Error getting product: {ex.Message}");
      return BadRequest($"Error getting product: {ex.Message}");
    }
  }

  [HttpGet("SearchAllProducts")]
  public ActionResult<List<ProductDTO>> SearchAllProducts(string searchedWord, string sortBy = "price", string sortOrder = "asc")
  {
    try
    {
      logger.LogInformation("Searching all products");
      var products = productService.SearchAllProducts(searchedWord, sortBy, sortOrder);
      return Ok(products);
    }
    catch (KeyNotFoundException knfex)
    {
      logger.LogWarning($"No products found: {knfex.Message}");
      return NotFound($"No products found: {knfex.Message}");
    }
    catch (ArgumentNullException anex)
    {
      logger.LogWarning($"Error searching products: {anex.Message}");
      return BadRequest($"Error searching services: {anex.Message}");
    }
    catch (Exception ex)
    {
      logger.LogError($"Error searching products: {ex.Message}");
      return BadRequest($"Error searching products: {ex.Message}");
    }
  }
  [Authorize]
  [HttpGet("SearchMyProducts")]
  public ActionResult<List<ProductDTO>> SearchMyProducts(string searchedWord, string sortBy = "Date", string sortOrder = "asc")
  {
    try
    {
      logger.LogInformation("Searching my products");
      var products = productService.SearchMyProducts(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, searchedWord, sortBy, sortOrder);
      return Ok(products);
    }
    catch (KeyNotFoundException knfex)
    {
      logger.LogWarning($"No products found: {knfex.Message}");
      return NotFound($"No products found: {knfex.Message}");
    }
    catch (ArgumentNullException anex)
    {
      logger.LogWarning($"Error searching products: {anex.Message}");
      return BadRequest($"Error searching services: {anex.Message}");
    }
    catch (Exception ex)
    {
      logger.LogError($"Error searching products: {ex.Message}");
      return BadRequest($"Error searching products: {ex.Message}");
    }
  }

  [Authorize]
  [HttpPost]
  public ActionResult<Product> Create([FromBody] ProductCreateDTO productCreateDTO)
  {
    if (!ModelState.IsValid) return BadRequest(ModelState);
    try
    {
      logger.LogInformation("Creating product");
      var product = productService.RegisterProduct(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, productCreateDTO);
      return CreatedAtAction(nameof(Get), new { productId = product.ProductId }, product);
    }
    catch (System.Text.Json.JsonException jex)
    {
      logger.LogWarning($"Invalid JSON format: {jex.Message}");
      return BadRequest($"Invalid JSON format: {jex.Message}");
    }
    catch (Exception ex)
    {
      logger.LogError($"Error creating product: {ex.Message}");
      return BadRequest(ex.Message);
    }
  }

  [Authorize]
  [HttpPut("{productId}")]
  public IActionResult Update(int productId, [FromBody] ProductUpdateDTO productUpdateDTO)
  {
    if (!ModelState.IsValid) return BadRequest(ModelState);
    try
    {
      logger.LogInformation("Updating product data");
      productService.UpdateProduct(User.FindFirst(ClaimTypes.Role)?.Value, User.FindFirst(ClaimTypes.NameIdentifier)?.Value, productId, productUpdateDTO);
      return Ok(productUpdateDTO);
    }
    catch (KeyNotFoundException nfex)
    {
      logger.LogWarning($"Product {productId} not found: {nfex.Message}");
      return NotFound($"Product {productId} not found: {nfex.Message}");
    }
    catch (System.Text.Json.JsonException jex)
    {
      logger.LogWarning($"Invalid JSON format: {jex.Message}");
      return BadRequest($"Invalid JSON format: {jex.Message}");
    }
    catch (UnauthorizedAccessException uaex)
    {
      logger.LogWarning(uaex.Message);
      return Unauthorized(uaex.Message);
    }
    catch (Exception ex)
    {
      logger.LogError($" Error updating product data: {ex.Message}");
      return BadRequest($" Error updating product data: {ex.Message}");
    }
  }

  [Authorize]
  [HttpDelete("{productId}")]
  public IActionResult Delete(int productId)
  {
    try
    {
      logger.LogInformation("Deleting product");
      productService.DeleteProduct(User.FindFirst(ClaimTypes.Role)?.Value, User.FindFirst(ClaimTypes.NameIdentifier)?.Value, productId);
      return NoContent();
    }
    catch (KeyNotFoundException knfex)
    {
      logger.LogWarning($"Product {productId} not found: {knfex.Message}");
      return NotFound($"Product {productId} not found: {knfex.Message}");
    }
    catch (UnauthorizedAccessException uaex)
    {
      logger.LogWarning(uaex.Message);
      return Unauthorized(uaex.Message);
    }
    catch (Exception ex)
    {
      logger.LogError($" Error deleting product: {ex.Message}");
      return BadRequest($" Error deleting product: {ex.Message}");
    }
  }
}