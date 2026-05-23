using E_Commerce_API.Enums;

namespace E_Commerce_API.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public double  TotalPrice { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
