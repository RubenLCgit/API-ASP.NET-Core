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
      var products = productService.GetAllProducts();
      return Ok(products);
    }
    catch (KeyNotFoundException knfex)
    {
      return NotFound($"No products found: {knfex.Message}");
    }
    catch (Exception ex)
    {
      return BadRequest($"Error getting all products: {ex.Message}");
    }
  }

  [HttpGet("{productId}")]
  public ActionResult<ProductDTO> Get(int productId)
  {
    try
    {
      var product = productService.GetProduct(productId);
      return Ok(product);
    }
    catch (KeyNotFoundException knfex)
    {
      return NotFound($"Product {productId} not found: {knfex.Message}");
    }
    catch (Exception ex)
    {
      return BadRequest($"Error getting product: {ex.Message}");
    }
  }

  [HttpGet("SearchAllProducts")]
  public ActionResult<List<ProductDTO>> SearchAllProducts(string searchedWord, string sortBy = "price", string sortOrder = "asc")
  {
    try
    {
      var products = productService.SearchAllProducts(searchedWord, sortBy, sortOrder);
      return Ok(products);
    }
    catch (KeyNotFoundException knfex)
    {
      return NotFound($"No products found: {knfex.Message}");
    }
    catch (ArgumentNullException anex)
    {
      return BadRequest($"Error searching services: {anex.Message}");
    }
    catch (Exception ex)
    {
      return BadRequest($"Error searching products: {ex.Message}");
    }
  }
  [Authorize]
  [HttpGet("SearchMyProducts")]
  public ActionResult<List<ProductDTO>> SearchMyProducts(string searchedWord, string sortBy = "Date", string sortOrder = "asc")
  {
    try
    {
      var products = productService.SearchMyProducts(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, searchedWord, sortBy, sortOrder);
      return Ok(products);
    }
    catch (KeyNotFoundException knfex)
    {
      return NotFound($"No products found: {knfex.Message}");
    }
    catch (ArgumentNullException anex)
    {
      return BadRequest($"Error searching services: {anex.Message}");
    }
    catch (Exception ex)
    {
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
      var product = productService.RegisterProduct(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, productCreateDTO);
      return CreatedAtAction(nameof(Get), new { productId = product.ProductId }, product);
    }
    catch (System.Text.Json.JsonException jex)
    {
      return BadRequest($"Invalid JSON format: {jex.Message}");
    }
    catch (Exception ex)
    {
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
      productService.UpdateProduct(User.FindFirst(ClaimTypes.Role)?.Value, User.FindFirst(ClaimTypes.NameIdentifier)?.Value, productId, productUpdateDTO);
      return Ok(productUpdateDTO);
    }
    catch (KeyNotFoundException nfex)
    {
      return NotFound($"Product {productId} not found: {nfex.Message}");
    }
    catch (System.Text.Json.JsonException jex)
    {
      return BadRequest($"Invalid JSON format: {jex.Message}");
    }
    catch (UnauthorizedAccessException uaex)
    {
      return Unauthorized(uaex.Message);
    }
    catch (Exception ex)
    {
      return BadRequest($" Error updating product data: {ex.Message}");
    }
  }

  [Authorize]
  [HttpDelete("{productId}")]
  public IActionResult Delete(int productId)
  {
    try
    {
      productService.DeleteProduct(User.FindFirst(ClaimTypes.Role)?.Value, User.FindFirst(ClaimTypes.NameIdentifier)?.Value, productId);
      return NoContent();
    }
    catch (KeyNotFoundException knfex)
    {
      return NotFound($"Product {productId} not found: {knfex.Message}");
    }
    catch (UnauthorizedAccessException uaex)
    {
      return Unauthorized(uaex.Message);
    }
    catch (Exception ex)
    {
      return BadRequest($" Error deleting product: {ex.Message}");
    }
  }
}