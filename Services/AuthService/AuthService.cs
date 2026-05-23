namespace E_Commerce_API.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> Login(string email, string password)
        {
            var user =  await _context.Users .SingleOrDefaultAsync(u => u.Email == email);

            if (user == null)
                return null;

            if (user.Password != password)
                return null;

            return user;
        }
    }
}
