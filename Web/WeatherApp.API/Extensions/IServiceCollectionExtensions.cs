using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using WeatherApp.API.Data.Database;
using WeatherApp.API.Services;

namespace WeatherApp.API.Extensions
{
    public static class IServiceCollectionExtensions
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IWeatherService, WeatherService>();

            return services;
        }


        public static IServiceCollection AddDevDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<WeatherAppDatabaseContext>(options => 
            {
                Log.Information("Configuring database");

                options.UseSqlServer(configuration.GetConnectionString("Default"));

            });

            return services;
        }

    }
}
