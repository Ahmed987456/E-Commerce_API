namespace E_Commerce_API.Dtos.CarDtos
{
    public class UpdateCartDto
    {
        public int UserId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
