using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace WeatherMap.API.StartupConfigurations
{
    public class MvcConfiguration
    {
        public static void AddMvc(IServiceCollection services)
        {
            services.AddMvcCore().AddApiExplorer();

            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.Formatting = Formatting.Indented;
            });
        }
    }
}
