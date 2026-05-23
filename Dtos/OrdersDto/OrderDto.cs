using E_Commerce_API.Enums;

namespace E_Commerce_API.Dtos.OrdersDto
{
    public class OrderDto
    {
        public int OrderId { get; set; }

        public double TotalPrice { get; set; }

        public OrderStatus OrderStatus { get; set; }
    }
}
