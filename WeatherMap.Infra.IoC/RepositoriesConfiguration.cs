using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeatherMap.Domain.Repositories;
using WeatherMap.Infra.Data;
using WeatherMap.Infra.Data.Repositories;

namespace WeatherMap.Infra.IoC
{
    public class RepositoriesConfiguration
    {
        public static void RegisterRepositories(IServiceCollection services, IConfiguration configuration)
        {
            var authConfig = new AuthConfig();
            configuration.GetSection("AuthConfig").Bind(authConfig);

            services.AddSingleton<IUserRepository>(UserRepository.Instance(authConfig));
        }
    }
}
