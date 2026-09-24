using CatalogService.Models;
using CatalogService.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Controllers;

    [ApiController]
    [Route("[controller]")]
    public class CatalogController : ControllerBase
    {
        private CatalogRepository _catalogRepository;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(ILogger<CatalogController> logger,  CatalogRepository catalogRepository)
        {
            _logger = logger;
            _catalogRepository =catalogRepository ;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            List<Product> catalog = await _catalogRepository.GetAll();
            return Ok(catalog);
        }
        
        [HttpGet("product/{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            try
            {
                _logger.LogDebug($"Getting product: {id}");

                var product = await _catalogRepository.GetById(id);

                if (product == null)
                {
                    return NotFound();
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting product: {id}");
                return BadRequest();
            }
        }

        [HttpPost("addproduct")]
        public async Task<ActionResult<Product>> AddProduct(Product product)
        {
            var addedProduct = await _catalogRepository.AddProduct(product);
            return Ok(addedProduct);
        }
        

        [HttpDelete("deleteproductbyid/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _catalogRepository.Delete(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
        
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest("Id i URL matcher ikke id i produktet.");
            }

            var success = await _catalogRepository.Update(id, product);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
        
    }