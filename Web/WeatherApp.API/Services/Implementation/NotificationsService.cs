using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.API.Data.Database;
using WeatherApp.API.Models.Entities;
using WeatherApp.API.Models.Request;
using WeatherApp.API.Services.Services;
using WeatherApp.API.SignalR.Hubs;

namespace WeatherApp.API.Services.Implementation
{
    public class NotificationsService : INotificationsService
    {
        private readonly WeatherAppDatabaseContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly ISignalRConnectionsManager _connectionManager;

        public NotificationsService(WeatherAppDatabaseContext dbContext, IMapper mapper, IHubContext<NotificationHub> hubContext, ISignalRConnectionsManager connectionManager)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _hubContext = hubContext;
            _connectionManager = connectionManager;
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

        public string GenerateUserID()
        {
            var id = Guid.NewGuid().ToString();


            if (_dbContext.Notifications.Any(notif => notif.UserID == id))
                GenerateUserID();

            return id;
        }

        public async Task<IEnumerable<Notification>> GetAllAsync() => await _dbContext.Notifications.ToListAsync();

        public async Task SendNotification<T>(string userID, T body)
        {
            var connections = _connectionManager.GetConnections(userID);

            if(connections != null & connections.Count > 0)
            {
                foreach (var connection in connections)
                {
                    await _hubContext.Clients.Client(connection).SendAsync("socket",body);
                }

                return;
            }

            Log.Debug($"No connection find for { userID}");

            throw new Exception($"No connection find for { userID}");

        }

       
    }
}
