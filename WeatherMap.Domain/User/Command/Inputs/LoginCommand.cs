using WeatherMap.Shared.Commands;

namespace WeatherMap.Domain.User.Command.Inputs
{
    public class LoginCommand : ICommand
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
