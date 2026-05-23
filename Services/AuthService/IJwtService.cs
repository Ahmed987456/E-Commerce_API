namespace E_Commerce_API.Services.AuthService
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
