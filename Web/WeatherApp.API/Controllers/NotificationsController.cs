using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Serilog;
using System;
using System.Threading.Tasks;
using WeatherApp.API.Models.Entities;
using WeatherApp.API.Models.Request;
using WeatherApp.API.Services.Services;

namespace WeatherApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationsService _notificationsService;
        private readonly LinkGenerator _linkGenerator;

        public NotificationsController(INotificationsService notificationsService, LinkGenerator linkGenerator)
        {
            _notificationsService = notificationsService;
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

        [HttpGet("send/{userID}")]
        public async Task SendNotifications(string userID)
        {
            await _notificationsService.SendNotification<string>(userID, "Message");
        }

    }
}
