namespace WeatherMap.Domain.Services
{
    public interface IUserService
    {
        string GenerateToken(User.Entities.User user);
    }
}
