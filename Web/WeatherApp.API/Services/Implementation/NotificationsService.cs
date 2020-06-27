using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherApp.API.Data.Database;
using WeatherApp.API.Models.Entities;
using WeatherApp.API.Models.Request;
using WeatherApp.API.Services.Services;

namespace WeatherApp.API.Services.Implementation
{
    public class NotificationsService : INotificationsService
    {
        private readonly WeatherAppDatabaseContext _dbContext;
        private readonly IMapper _mapper;

        public NotificationsService(WeatherAppDatabaseContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<bool> AddNotificationsAsync(NotificationRequestModel notification)
        {
            if(notification == null)
            {
                Log.Debug($"Param: {notification} was null");

                throw new ArgumentNullException(nameof(notification), "Notification param was null");
            }

            var notificationEntity = _mapper.Map<Notification>(notification);

             _dbContext.Add(notificationEntity);

            if (await _dbContext.SaveChangesAsync() > 0)
                return true;

            return false;

        }

        public async Task<IEnumerable<Notification>> GetAllAsync() => await _dbContext.Notifications.ToListAsync();


    }
}
