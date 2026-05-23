using E_Commerce_API.Dtos.CarDtos;
using E_Commerce_API.Services.CarServices;
using E_Commerce_API.Services.ProductService;
using E_Commerce_API.Services.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarItemsController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICarService _carService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;


        public CarItemsController(IUserService userService, ICarService carService, IProductService productService, IMapper mapper)
        {
            _userService = userService;
            _carService = carService;
            _productService = productService;
            _mapper = mapper;
        }

        [HttpPost]

        public async Task<IActionResult> AddItemForCar([FromForm] CreateCartItemDto dto) 
        {
            var User = await _userService.GetUserById(dto.UserId);
            if (User == null) 
               return NotFound("Not User Found With This Id");

            var Product = await _productService.GetById(dto.ProductId);
            if (Product == null)
                return NotFound("Not Product Found With This Id");

            if (Product.StockQuantity <= 0)
                return BadRequest(error: "There is not enough of the product.");

            if (dto.Quantity > Product.StockQuantity)
                return BadRequest("The quantity requested exceeds the available stock.");
            await _carService.CreateCarItem(dto);
            return Ok("Add Succeded");
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCartAsync(int userId)
        {
            var user = await _userService.GetUserById(userId);

            if (user == null)
                return NotFound("User not found");

            var result = await _carService.GetUserCart(userId);
            return Ok(result);
        }

        [HttpDelete]

        public async Task<IActionResult> DeleteItemFromCart(int ProductId, int UserId) 
        {
            var item = await _carService.GetCartItem(ProductId, UserId);

            if (item == null)
                return NotFound("Item not found in cart");
            await _carService.DeleteCarItem(item);
            return NoContent();
        }
        [HttpPut]

        public async Task<IActionResult> UpdateCartQuantity([FromForm] UpdateCartDto dto) 
        {
            var User = await _userService.GetUserById(dto.UserId);
            if (User == null)
                return NotFound("Not User Found With This Id");

            var Product = await _productService.GetById(dto.ProductId);
            if (Product == null)
                return NotFound("Not Product Found With This Id");

            var item = await _carService.GetCartItem( dto.UserId, dto.ProductId);
            if (item == null)
                return BadRequest("This product is not in the basket.");

            if (dto.Quantity > Product.StockQuantity)
                return BadRequest("The quantity requested exceeds the available stock.");

             _mapper.Map(dto, item);
            await _carService.UpdateCarItemQuantity(item);
            return Ok("Cart Updated Successfully");
        }
    }
}
