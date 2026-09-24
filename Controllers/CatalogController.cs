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
        
        [HttpGet("product/{Id}")]
        public async Task<ActionResult<Product>> GetById(int Id)
        {
            List<Product> catalog = await _catalogRepository.GetAll();
            try
            {
                _logger.LogDebug($"Getting product: {Id}");

                var product = catalog.FirstOrDefault(product => product.Id == Id);

                if (product == null)
                {
                    return NotFound();
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting product: {Id}");
                return BadRequest();
            }
        }

        [HttpPost("addproduct")]
        public async Task<ActionResult<Product>> AddProduct(Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            var addedProduct = await _catalogRepository.Add(product);
            return Ok(addedProduct);
        }
        

        [HttpDelete]
        [Route("deleteproductbyid/{id}")]
        public async Task<ActionResult<Product>> Delete(int id)
        {
            Product product = await _catalogRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            await _catalogRepository.Delete(id);
            return NoContent();
        } 
        
        [HttpPut("update/{id}")]
        public async Task<ActionResult<Product>> Update(int id, Product product)
        {
            Product existing = await _catalogRepository.GetById(id);
            if (existing == null)
            {
                return NotFound();
            }

            await _catalogRepository.Update(product);
            return Ok(product);
        }
        
    }