using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherApp.API.Models.Entities;
using WeatherApp.API.Models.Request;

namespace WeatherApp.API.Services.Services
{
    public interface INotificationsService
    {
        Task<IEnumerable<Notification>> GetAllAsync();
        Task<bool> AddNotificationsAsync(NotificationRequestModel notification);
        Task SendNotification<T>(string userID, T body);
        string GenerateUserID();
        string GetUserID(string email);
    }
}
