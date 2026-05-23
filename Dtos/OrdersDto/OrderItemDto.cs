namespace E_Commerce_API.Dtos.OrdersDto
{
    public class OrderItemDto
    {
        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public double ItemTotal { get; set; }
    }
}
