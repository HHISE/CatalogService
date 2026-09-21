using System.ComponentModel.DataAnnotations;

namespace CatalogService.Models;

public class Product
{
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
}