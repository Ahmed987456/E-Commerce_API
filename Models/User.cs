using E_Commerce_API.Enums;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce_API.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required , MaxLength(100)]
        public string Name { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }

        public UserRole Role { get; set; }  

        public ICollection<Order> Orders { get; set; } = new List<Order>();

        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    }
}
