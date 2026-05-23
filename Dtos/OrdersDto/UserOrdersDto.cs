using E_Commerce_API.Enums;

namespace E_Commerce_API.Dtos.OrdersDto
{
    public class UserOrdersDto
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public double TotalPrice { get; set; }

        public OrderStatus OrderStatus { get; set; }
    }
}
