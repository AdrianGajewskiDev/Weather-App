using System.Threading.Tasks;
using WeatherApp.API.Models;
using WeatherApp.API.Models.WeatherDetails;

namespace WeatherApp.API.Services
{
    public interface IWeatherService
    {
        Task<ApiResponse<WeatherModel>> GetWeatherByCityNameAsync(string cityName);
        Task<ApiResponse<WeatherModel>> GetWeatherByCityIDAsync(int cityID);
        Task<ApiResponse<WeatherModel>> GetWeatherByCityCoordinatesAsync(Coord coordinates);

    }
}
