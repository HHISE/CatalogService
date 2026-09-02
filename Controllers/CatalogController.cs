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
                Brand = "Apple",
                Manufacturer = "Apple Inc.",
                Model = "A3288",
                ImageUrl = "https://example.com/iphone16.jpg",
                ProductUrl = "https://example.com/iphone16",
                ReleaseDate = new DateTime(2024, 9, 20),
                ExpiryDate = null
            },

            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Galaxy S25",
                Description = "Samsung smartphone med AMOLED-skærm og kraftig processor.",
                Price = 6999.00m,
                Brand = "Samsung",
                Manufacturer = "Samsung Electronics",
                Model = "SM-S931B",
                ImageUrl = "https://example.com/galaxys25.jpg",
                ProductUrl = "https://example.com/galaxys25",
                ReleaseDate = new DateTime(2025, 2, 7),
                ExpiryDate = null
            },

            new Product
            {
                Id = Guid.NewGuid(),
                Name = "MacBook Air M4",
                Description = "Let og kraftfuld bærbar computer med Apple M4-chip.",
                Price = 9499.00m,
                Brand = "Apple",
                Manufacturer = "Apple Inc.",
                Model = "MacBook Air 13",
                ImageUrl = "https://example.com/macbookair.jpg",
                ProductUrl = "https://example.com/macbookair",
                ReleaseDate = new DateTime(2025, 3, 12),
                ExpiryDate = null
            }
        ];

        public CatalogController(ILogger<CatalogController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet("productindex/{index}", Name = "GetProductAtIndex")]
        public ActionResult<Product> Get(int index)
        {
            try
            {
                _logger.LogDebug($"Getting product index");
                var product = Catalog.GetValue(index);
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting product index");
                return BadRequest();
            }
            
        }
    }