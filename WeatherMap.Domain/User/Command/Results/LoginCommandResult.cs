using WeatherMap.Shared.Commands;

namespace WeatherMap.Domain.User.Command.Results
{
    public class LoginCommandResult : ICommandResult
    {
        public string Username { get; set; }
        public string Token { get; set; }
        
        public LoginCommandResult() { }

        public LoginCommandResult(string username, string token)
        {
            Username = username;
            Token = token;        
        }
    }
}
