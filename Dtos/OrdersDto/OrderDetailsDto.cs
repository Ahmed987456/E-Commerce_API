using E_Commerce_API.Enums;

namespace E_Commerce_API.Dtos.OrdersDto
{
    public class OrderDetailsDto
    {
      
            public int OrderId { get; set; }

            public double TotalPrice { get; set; }

            public OrderStatus OrderStatus { get; set; }

            public List<OrderItemDto> Items { get; set; }

    }
}
