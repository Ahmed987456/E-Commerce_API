using E_Commerce_API.Dtos.UserDtos;

namespace E_Commerce_API.Services.UserService
{
    public interface IUserService
    {
        Task<User?> GetUserById(int id);

        Task<bool> Emailconfirmation(string Email);

        Task<User> CreateUser(User user);

        Task<List<UserDto>> GetAllUsers();

        Task<bool> EmailExistsForUpdate(string email, int id);

        Task UpdateUser(User user);

        Task DeleteUser(User user);    
    }
}
