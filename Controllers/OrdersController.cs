using E_Commerce_API.Dtos.OrdersDto;
using E_Commerce_API.Services.OrderService;
using E_Commerce_API.Services.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService, IUserService userService)
        {
            _orderService = orderService;
            _userService = userService;
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder(int userId)
        {
            var user = await _userService.GetUserById(userId);

            if (user == null)
                return NotFound("User not found");

            var order = await _orderService.CreateOrder(userId);

            if (order == null)
                return BadRequest("Cart is empty or quantity unavailable");

            return Ok(new OrderDto
            {
                OrderId = order.Id,
                TotalPrice = order.TotalPrice,
                OrderStatus = order.OrderStatus
            });
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetAllUserOrders(int id) 
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
                return NotFound("Not User Found With This Id");
            var orders = await _orderService.GetAllUserOrders(id);
            return Ok(orders);
        }

        [HttpGet("orderdetails/{orderId}")]

        public async Task<IActionResult> OrderDetailsAsync(int orderId) 
        {
          var order = await _orderService.GetOrderDetails(orderId);
            if (order == null)
                return NotFound("Not Order Found With This Id");
          return Ok(order);
        }

        [HttpPut("UpdateStatus/{orderId}")]

        public async Task<IActionResult> UpdateOraderStatus(int orderId,[FromForm] UpdateOrderStatusDto dto) 
        {
            var order = await _orderService.GetOrderById(orderId);
            if (order == null)
                return NotFound("Not Order Found With This Id");
            order.OrderStatus = dto.OrderStatus;
            await _orderService.UpdateStatus();
            return Ok("Order Status Updated Successfully");
        }

        [HttpPut("CancelOrder/{orderid}")]

        public async Task<IActionResult> CancelOrderAsync(int orderid) 
        {
            var order = await _orderService.GetOrderById(orderid);

            if (order == null)
                return NotFound("Not Order Found With This Id");

            var result = await _orderService.CancelOrder(orderid);

            if (result != "Success")
                return BadRequest(result);

            return Ok("Order Cancelled Successfully");
        }
    }
}
