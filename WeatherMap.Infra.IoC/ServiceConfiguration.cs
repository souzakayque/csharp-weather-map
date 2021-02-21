using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeatherMap.Domain.Services;
using WeatherMap.Infra.Data;
using WeatherMap.Infra.Services;

namespace WeatherMap.Infra.IoC
{
    internal class ServiceConfiguration
    {
        internal static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            var weatherMapConfig = new WeatherMapConfig();
            configuration.GetSection("weatherMapConfig").Bind(weatherMapConfig);
            services.AddSingleton<IWeatherMapService>(WeatherMapService.Instance(weatherMapConfig));

            var userConfig = new UserConfig();
            configuration.GetSection("userConfig").Bind(userConfig);
            services.AddSingleton<IUserService>(UserService.Instance(userConfig));
        }
    }
}
