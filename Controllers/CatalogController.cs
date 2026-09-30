using CatalogService.Models;
using CatalogService.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CatalogService.Controllers;

    [ApiController]
    [Route("[controller]")]
    public class CatalogController : ControllerBase
    {
        private ICatalogRepository _catalogRepository;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(ILogger<CatalogController> logger,  ICatalogRepository catalogRepository)
        {
            _logger = logger;
            _catalogRepository =catalogRepository ;
        }
        
        [HttpGet("version")]
        public async Task<Dictionary<string,string>> GetVersion()
        {
            var properties = new Dictionary<string, string>();
            var assembly = typeof(Program).Assembly;
            properties.Add("service", "HaaV Catalog Service"); // eller "HaaV Catalog Service
            var ver = FileVersionInfo.GetVersionInfo(typeof(Program)
                .Assembly.Location).ProductVersion;
            properties.Add("version", ver!);
            try {
                var hostName = System.Net.Dns.GetHostName();
                var ips = await System.Net.Dns.GetHostAddressesAsync(hostName);
                var ipa = ips.First().MapToIPv4().ToString();
                properties.Add("hosted-at-address", ipa);
            } catch (Exception ex) {
                _logger.LogError(ex.Message);
                properties.Add("hosted-at-address", "Could not resolve IP-address");
            }
            return properties;
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