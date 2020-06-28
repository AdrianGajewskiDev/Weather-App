using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using WeatherApp.API.Data.Database;
using WeatherApp.API.Models.Entities;
using WeatherApp.API.Models.Request;
using WeatherApp.API.Services;
using WeatherApp.API.Services.Implementation;
using WeatherApp.API.Services.Services;

namespace WeatherApp.API.Extensions
{
    public static class IServiceCollectionExtensions
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IWeatherService, WeatherService>();
            services.AddScoped<INotificationsService, NotificationsService>();
            services.AddSingleton<ISignalRConnectionsManager, SignalRConnectionsManager>();

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


        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.CreateMap<NotificationRequestModel, Notification>();
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }



        public static IServiceCollection AddAndConfigureSignalR(this IServiceCollection services)
        {
            services.AddSignalR(configure => 
            {
                configure.ClientTimeoutInterval = TimeSpan.FromMinutes(60);
            });

            return services; 
        }
    }
}
