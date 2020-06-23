using Microsoft.Extensions.Options;
using Serilog;
using System;
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


        public async Task<ApiResponse<WeatherModel>> GetWeatherByCityCoordinatesAsync(Coord coordinates)
        {
            if (coordinates == null)
            {
                Log.Error("City coordinates are null");

                throw new ArgumentNullException();
            }
            var requestURl = _applicationData.OWMUrl + $"lat={coordinates.Lat}&lon={coordinates.Lon}&" + "appid=" + _applicationData.OWMApiKey;
            var response = await _clientService.GetAsync<WeatherModel>(requestURl);

            return response;
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
