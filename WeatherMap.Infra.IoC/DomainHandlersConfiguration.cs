using Microsoft.Extensions.DependencyInjection;
using WeatherMap.Domain.User.Command.Handlers;
using WeatherMap.Domain.WeatherMap.Command.Handlers;

namespace WeatherMap.Infra.IoC
{
    public class DomainHandlersConfiguration
    {
        public static void RegisterDomainHandlers(IServiceCollection services)
        {
            services.AddScoped<IWeatherMapCommandHandler, WeatherMapCommandHandler>();
            services.AddScoped<IUserCommandHandler, UserCommandHandler>();
        }
    }
}
