using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using WeatherApp.API.Data;
using WeatherApp.API.Models;
using WeatherApp.API.Services;

namespace WeatherApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly ApplicationData _applicationData;

        public WeatherController(IClientService clientService, IOptions<ApplicationData> _options)
        {
            _clientService = clientService;
            _applicationData = _options.Value;
        }

        //api/weather/currentweather
        [HttpGet("currentWeather")]
        public async Task<IActionResult> GetCurrentWeather()
        {
            var response = await _clientService.GetAsync<WeatherModel>(_applicationData.OWMUrl + $"q=Gdansk&" + "appid=" +_applicationData.OWMApiKey);

            return Ok(response);
        }
    }
}
