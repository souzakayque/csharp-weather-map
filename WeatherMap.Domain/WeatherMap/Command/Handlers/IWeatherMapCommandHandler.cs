using WeatherMap.Domain.WeatherMap.Command.Inputs;
using WeatherMap.Shared.Commands;

namespace WeatherMap.Domain.WeatherMap.Command.Handlers
{
    public interface IWeatherMapCommandHandler : 
        ICommandHandler<WeatherMapCommand>,
        ICommandHandler<WeatherMapPeriodicallyCommand>
    {
    }
}
