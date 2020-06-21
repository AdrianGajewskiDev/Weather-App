using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using WeatherApp.API.Data;
using WeatherApp.API.Models;
using WeatherApp.API.Models.WeatherDetails;

namespace WeatherApp.API.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IClientService _clientService;
        private readonly ApplicationData _applicationData;


        public WeatherService(IClientService clientService, IOptions<ApplicationData> applicationData)
        {
            _clientService = clientService;
            _applicationData = applicationData.Value;
        }


        public Task<ApiResponse<WeatherModel>> GetWeatherByCityCoordinatesAsync(Coord coordinates)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ApiResponse<WeatherModel>> GetWeatherByCityIDAsync(int cityID)
        {
            var response = await _clientService.GetAsync<WeatherModel>(_applicationData.OWMUrl + $"id={cityID}&" + "appid=" + _applicationData.OWMApiKey);

            return response;
        }

        public async Task<ApiResponse<WeatherModel>> GetWeatherByCityNameAsync(string cityName)
        {
            var response = await _clientService.GetAsync<WeatherModel>(_applicationData.OWMUrl + $"q={cityName}&" + "appid=" + _applicationData.OWMApiKey);

            return response;
        }
    }
}
