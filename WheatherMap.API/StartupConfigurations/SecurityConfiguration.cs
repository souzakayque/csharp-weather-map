using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WeatherMap.API.StartupConfigurations
{
    public class SecurityConfiguration
    {
        public static void AddAuthentication(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = "https://" + configuration["Auth0:Domain"];
                options.Audience = configuration["Auth0:ApiIdentifier"];
                options.RequireHttpsMetadata = false;
            });
        }
    }
}
