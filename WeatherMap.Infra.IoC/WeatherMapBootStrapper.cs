using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WeatherMap.Infra.IoC
{
    public class WeatherMapBootStrapper
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            DomainHandlersConfiguration.RegisterDomainHandlers(services);

            ServiceConfiguration.RegisterServices(services, configuration);

            RepositoriesConfiguration.RegisterRepositories(services, configuration);
        }
    }
}
