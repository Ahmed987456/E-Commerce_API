using E_Commerce_API.Dtos.UserDtos;
using E_Commerce_API.Models;
using E_Commerce_API.Services.CarServices;
using E_Commerce_API.Services.OrderService;
using E_Commerce_API.Services.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;
        private readonly ICarService _carService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper, IOrderService orderService, ICarService carService)
        {
            _userService = userService;
            _mapper = mapper;
            _orderService = orderService;
            _carService = carService;
        }

        //CreateNewUser
        [HttpPost]

        public async Task<IActionResult> CreatUserAsync([FromForm] CreateUserDto dto)
        {
            var exisitEmail = await _userService.Emailconfirmation(dto.Email);
            if (exisitEmail)
                return BadRequest("The email already exists");
            var user = _mapper.Map<User>(dto);

            await _userService.CreateUser(user);

            return Ok(_mapper.Map<UserDto>(user));
        }

        //GetAllUser
        [HttpGet]

        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        //GetUserById
        [HttpGet("{UserId}")]

        public async Task<IActionResult> GetUserByIdAsync(int UserId)
        {
            var user = await _userService.GetUserById(UserId);
            if (user == null)
                return NotFound("Not User Found With This Id");

            return Ok(_mapper.Map<UserDto>(user));
        }

        //UpdateUser
        [HttpPut("{UserId}")]

        public async Task<IActionResult> UpdateUser(int UserId, [FromForm] UpdateUserDto dto)
        {
            var user = await _userService.GetUserById(UserId);

            if (user == null)
                return NotFound("Not User Found With This Id");
            if (!string.IsNullOrEmpty(dto.Email))
            {
                var emailExists = await _userService.EmailExistsForUpdate(dto.Email, UserId);
                if (emailExists)
                    return BadRequest("Email already exists");
            }

            _mapper.Map(dto, user);
            await _userService.UpdateUser(user);
            return Ok("Modified successfully");
        }

        //DeleteUser
        [HttpDelete("{UserId}")]

        public async Task<IActionResult> DeleteUserAsync(int UserId) 
        {
            var user = await _userService.GetUserById(UserId);

            if (user == null)
                return NotFound("Not User Found With This Id");

            var order = await _orderService.HasOrders(UserId);

            if (order)
                return BadRequest("Cannot delete user because he has orders");

            await _carService.DeleteUserCartItems(UserId);

            await _userService.DeleteUser(user);

            return NoContent();
        }

    }
}
