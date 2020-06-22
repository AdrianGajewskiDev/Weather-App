using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WeatherApp.API.Models;
using WeatherApp.API.Models.WeatherDetails;
using WeatherApp.API.Services;

namespace WeatherApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }


        //api/weather/currentweather
        [HttpGet("currentWeatherByCity/{city}")]
        public async Task<ActionResult<ApiResponse<WeatherModel>>> GetCurrentWeatherByCityName(string city)
        {
            var response = await _weatherService.GetWeatherByCityNameAsync(city);

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest || response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound($"Cannot find a weather for {city}");

            return response;
        }


        //api/weather/currentweather
        [HttpGet("currentWeatherByCityID/{id}")]
        public async Task<ActionResult<ApiResponse<WeatherModel>>> GetCurrentWeatherByCityID(int id)
        {
            var response = await _weatherService.GetWeatherByCityIDAsync(id);

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest || response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound($"Cannot find city with id of {id}");

            return response;
        }

        //api/weather/currentweather
        [HttpGet("currentWeatherByCityCoord")]
        public async Task<ActionResult<ApiResponse<WeatherModel>>> GetCurrentWeatherByCityCoord([FromQuery]Coord coord)
        {
         

            var response = await _weatherService.GetWeatherByCityCoordinatesAsync(coord);

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest || response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound($"Cannot find city with coords of {coord.ToString()}");

            return response;
        }


    }
}
