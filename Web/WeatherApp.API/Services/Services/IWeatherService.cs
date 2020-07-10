using System.Threading.Tasks;
using WeatherApp.API.Models;
using WeatherApp.API.Models.WeatherDetails;

namespace WeatherApp.API.Services
{
    public interface IWeatherService
    {
        Task<WeatherModel> GetWeatherByCityNameAsync(string cityName);
        Task<WeatherModel> GetWeatherByCityIDAsync(int cityID);
        Task<WeatherModel> GetWeatherByCityCoordinatesAsync(Coord coordinates);

    }
}
