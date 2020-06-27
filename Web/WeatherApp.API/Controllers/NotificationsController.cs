using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WeatherApp.API.Models.Request;

namespace WeatherApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        [HttpPost("registerNotification")]
        public async Task<IActionResult> RegisterNewNotification(NotificationRequestModel model)
        {


            return Ok();
        }
    }
}
