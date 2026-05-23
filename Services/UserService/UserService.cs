

using E_Commerce_API.Dtos.UserDtos;
using E_Commerce_API.Models;

namespace E_Commerce_API.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UserService(AppDbContext context, IMapper mapper = null)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<User> CreateUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task DeleteUser(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Emailconfirmation(string Email)
        {
         return await _context.Users.AnyAsync(u => u.Email.ToLower() == Email.ToLower()); 
        }

        public async Task<bool> EmailExistsForUpdate(string email, int id)
        {
            return await _context.Users.AnyAsync(
                u => u.Email.ToLower() == email.ToLower()
                && u.Id != id
            );
        }

        public async Task<List<UserDto>> GetAllUsers()
        {
           var Users =  await _context.Users.ToListAsync();

           return _mapper.Map<List<UserDto>>(Users);
        }

        public async Task<User?> GetUserById(int id)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
