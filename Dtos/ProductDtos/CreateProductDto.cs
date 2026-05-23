using System.ComponentModel.DataAnnotations;

namespace E_Commerce_API.Dtos.ProductDtos
{
    public class CreateProductDto
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        [Range(0, 999999)]
        public double Price { get; set; }

        [Range(0, 999999)]
        public int StockQuantity { get; set; }

        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }
    }
}
