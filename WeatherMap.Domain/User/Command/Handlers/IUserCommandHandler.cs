using WeatherMap.Domain.User.Command.Inputs;
using WeatherMap.Shared.Commands;

namespace WeatherMap.Domain.User.Command.Handlers
{
    public interface IUserCommandHandler :
        ICommandHandler<LoginCommand>
    {
    }
}
