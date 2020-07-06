using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Serilog;
using System.Threading;
using System.Threading.Tasks;
using WeatherApp.API.Models;
using WeatherApp.API.Models.Request;
using WeatherApp.API.Services;
using WeatherApp.API.Services.Services;

namespace WeatherApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationsService _notificationsService;
        private readonly IWeatherService _weatherService;
        private readonly LinkGenerator _linkGenerator;

        public NotificationsController(INotificationsService notificationsService , IWeatherService weatherService, LinkGenerator linkGenerator)
        {
            _notificationsService = notificationsService;
            _weatherService = weatherService;
            _linkGenerator = linkGenerator;
        }

        [HttpPost("registerNotification")]
        public async Task<IActionResult> RegisterNewNotification(NotificationRequestModel model)
        {
            Log.Information($"Processing request from {Request.Headers["Origin"]}");

            if (model == null)
            {
                Log.Debug("Model was null");
                return BadRequest("Request model was null");
            }

            model.UserID = _notificationsService.GenerateUserID();

            if (await _notificationsService.AddNotificationsAsync(model) == true)
            {
                return Created(_linkGenerator.GetPathByAction("RegisterNewNotification", "Notifications"), model);
            }

            Log.Debug("Something bad has happened while trying to add new notification");

            return BadRequest("Something bad has happened while trying to add new notification");
        }

        [HttpGet("getUserID/{email}")]
        public ActionResult<string> GetUserID(string email)
        {
            string userID = _notificationsService.GetUserID(email);

            if (userID == null)
                return NotFound($"Cannot find user with email: { email }");

            return Ok(userID);
        }

        [HttpGet("send")]
        public async Task SendNotifications()
        {
            while(true)
            {
                var users = await _notificationsService.GetAllAsync();

                if (users == null) break;

                foreach (var user in users)
                {
                    var weather = await _weatherService.GetWeatherByCityNameAsync(user.RequestedCityName);

                    await _notificationsService.SendNotification<ApiResponse<WeatherModel>>(user.UserID, weather);
                }
                Thread.Sleep(5000);
            }
        }

    }
}
