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
                ProductId = Guid.NewGuid(),
                Name = "iPhone 16",
                Description = "Apple smartphone med avanceret kamera og OLED-skærm.",
                Price = 7999.00m,
                ImageUrl = "https://example.com/iphone16.jpg",
            },

            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Galaxy S25",
                Description = "Samsung smartphone med AMOLED-skærm og kraftig processor.",
                Price = 6999.00m,
                ImageUrl = "https://example.com/galaxys25.jpg",

            },

            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "MacBook Air M4",
                Description = "Let og kraftfuld bærbar computer med Apple M4-chip.",
                Price = 9499.00m,
                ImageUrl = "https://example.com/macbookair.jpg",
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
        
        [HttpGet("product/{productId}", Name = "GetProductById")]
        public ActionResult<Product> Get(Guid productId)
        {
            try
            {
                _logger.LogDebug($"Getting product: {productId}");

                var product = Catalog.FirstOrDefault(p => p.Id == productId);

                if (product == null)
                {
                    return NotFound();
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting product: {productId}");
                return BadRequest();
            }
        }
    }