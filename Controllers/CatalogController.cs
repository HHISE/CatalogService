using CatalogService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Controllers;

    [ApiController]
    [Route("[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly ILogger<CatalogController> _logger;
        
        
        private static readonly Product[] Catalog =
        [
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "iPhone 16",
                Description = "Apple smartphone med avanceret kamera og OLED-skærm.",
                Price = 7999.00m,
            },

            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Galaxy S25",
                Description = "Samsung smartphone med AMOLED-skærm og kraftig processor.",
                Price = 6999.00m,

            },

            new Product
            {
                Id = Guid.NewGuid(),
                Name = "MacBook Air M4",
                Description = "Let og kraftfuld bærbar computer med Apple M4-chip.",
                Price = 9499.00m,
            }
        ];

        public CatalogController(ILogger<CatalogController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet]
        public IEnumerable<Product> GetAll()
        {
            return Catalog;
        }
        
        [HttpGet("product/{Id}")]
        public ActionResult<Product> Get(Guid Id)
        {
            try
            {
                _logger.LogDebug($"Getting product: {Id}");

                var product = Catalog.FirstOrDefault(product => product.Id == Id);

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
    }