namespace E_Commerce_API.Services.AuthService
{
    public interface IAuthService
    {
        Task<User?> Login(string email, string password);
    }
}
