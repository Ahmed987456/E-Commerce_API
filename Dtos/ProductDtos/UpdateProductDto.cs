using System.ComponentModel.DataAnnotations;

namespace E_Commerce_API.Dtos.ProductDtos
{
    public class UpdateProductDto
    {
 
        public string? Name { get; set; }

        public string? Description { get; set; }

        public double? Price { get; set; }

        public int? StockQuantity { get; set; }

        public string? ImageUrl { get; set; }

        public int? CategoryId { get; set; }
    }
}
