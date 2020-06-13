using Microsoft.Extensions.DependencyInjection;
using WeatherApp.API.Services;

namespace WeatherApp.API.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IClientService, ClientService>();

            return services;
        }
    }
}
