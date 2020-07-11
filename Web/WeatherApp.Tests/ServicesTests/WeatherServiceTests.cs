using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherApp.API.Data;
using WeatherApp.API.Helpers;
using WeatherApp.API.Models;
using WeatherApp.API.Services;
using Xunit;

namespace WeatherApp.Tests.ServicesTests
{
    public class WeatherServiceTests
    {
        [Theory]
        [InlineData("Tczew")]
        [InlineData("Gdansk")]
        [InlineData("Berlin")]
        [InlineData("Subkowy")]
        [InlineData("London")]
        public async Task Weather_Service_Should_Return_Weather_By_City_Name(string name)
        {
            ApplicationData applicationData = new ApplicationData
            {
                OWMUrl = "https://api.openweathermap.org/data/2.5/weather?",
                OWMApiKey = "f1b58a47aa129daa6330e7280a88e7b5"
            };

            var options = Options.Create<ApplicationData>(applicationData);

            var mockFactory = new Mock<IHttpClientFactory>();
            var client = new HttpClient();
            mockFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(client);
            IHttpClientFactory factory = mockFactory.Object;

            IClientService clientService = new ClientService(factory);
            IWeatherService weatherService = new WeatherService(clientService, options);


            var result =await weatherService.GetWeatherByCityNameAsync(name);

            Assert.NotNull(result);
            Assert.IsType<WeatherModel>(result);
        }

        [Theory]
        [InlineData(3083426)]
        [InlineData(7531002)]
        [InlineData(2820565)]
        [InlineData(2820582)]
        public async Task Weather_Service_Should_Return_Weather_By_City_ID(int id)
        {
            ApplicationData applicationData = new ApplicationData
            {
                OWMUrl = "https://api.openweathermap.org/data/2.5/weather?",
                OWMApiKey = "f1b58a47aa129daa6330e7280a88e7b5"
            };

            var options = Options.Create<ApplicationData>(applicationData);

            var mockFactory = new Mock<IHttpClientFactory>();
            var client = new HttpClient();
            mockFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(client);
            IHttpClientFactory factory = mockFactory.Object;

            IClientService clientService = new ClientService(factory);
            IWeatherService weatherService = new WeatherService(clientService, options);


            var result = await weatherService.GetWeatherByCityIDAsync(id);

            Assert.NotNull(result);
            Assert.IsType<WeatherModel>(result);
        }

        [Theory]
        [InlineData("Tczew")]
        [InlineData("Gdansk")]
        [InlineData("Berlin")]
        [InlineData("Subkowy")]
        [InlineData("London")]
        public async Task Weather_Service_Should_Return_Weather_Only_One_Forecast_For_Specified_Date(string cityName)
        {
            ApplicationData applicationData = new ApplicationData
            {
                OWMUrl = "https://api.openweathermap.org/data/2.5/weather?",
                OWMApiKey = "f1b58a47aa129daa6330e7280a88e7b5",
                OWMForecastUrl = "http://api.openweathermap.org/data/2.5/forecast?"
            };

            Random random = new Random();
            var options = Options.Create<ApplicationData>(applicationData);
            var mockFactory = new Mock<IHttpClientFactory>();
            var client = new HttpClient();
            mockFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(client);
            IHttpClientFactory factory = mockFactory.Object;
            IClientService clientService = new ClientService(factory);
            IWeatherService weatherService = new WeatherService(clientService, options);

            var result = await weatherService.GetLongWeatherForecastAsync(cityName);
            var date = result.List.ToList()[0].WeatherForecastDateTime.Date.ToShortDateString();

            var res = WeatherForecastHelper.GetFilteredItems( result.List);
            var res2 = WeatherForecastHelper.GetFilteredItems(result.List).Skip(1);
            Assert.Equal(1, res.Where(res => res.WeatherForecastDateTime.Date.ToShortDateString() == date).Count());
            Assert.True(!res2.Any(res => res.WeatherForecastDateTime.Date.ToShortDateString() == date));
        }
    }
}
