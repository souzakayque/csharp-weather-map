using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ServiceStack;
using System.Text;
using WeatherMap.Infra.IoC;
using WeatherMap.Shared.Common;

namespace WheatherMap.API
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }


        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            var key = Encoding.ASCII.GetBytes(RunTime.key);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            WeatherMap.API.StartupConfigurations.MvcConfiguration.AddMvc(services);

            // Add functionality to inject IOptions<T>
            services.AddOptions();
            services.AddLogging();

            // Add our Config object so it can be injected
            services.AddSingleton<IConfiguration>(Configuration);

            //WeatherMap.API.StartupConfigurations.SecurityConfiguration.AddAuthentication(services, Configuration);
            WeatherMap.API.StartupConfigurations.SwaggerConfiguration.AddSwagger(services);

            WeatherMapBootStrapper.RegisterServices(services, Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
            );

            app.UseMvc();

            app.UseRewriter(new RewriteOptions().AddRedirect("(.*)docs$", "$1docs/index.html"));

            WeatherMap.API.StartupConfigurations.SwaggerConfiguration.UseSwagger(app);
        }
    }
}
