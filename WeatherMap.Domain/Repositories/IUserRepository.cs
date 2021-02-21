namespace WeatherMap.Domain.Repositories
{
    public interface IUserRepository
    {
        User.Entities.User Authentication(string user, string password);
    }
}
