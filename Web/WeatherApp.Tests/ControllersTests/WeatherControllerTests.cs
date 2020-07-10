using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherApp.API.Controllers;
using WeatherApp.API.Data;
using WeatherApp.API.Models;
using WeatherApp.API.Services;
using WeatherApp.Tests.SampleWeather;
using Xunit;

namespace WeatherApp.Tests.ControllersTests
{
    public class WeatherControllerTests
    {
        

        [Theory()]
        [InlineData("Tczew", Paths.TczewWeather)]
        [InlineData("Gdansk", Paths.GdanskWeather)]
        public async Task Weather_Controller_Should_Return_Weather_By_City_Name(string city, string path)
        {
            await updateWeatherJson(path, city);
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
            WeatherController controller = new WeatherController(weatherService);

            var result = await controller.GetCurrentWeatherByCityName(city);

            var json = File.ReadAllText(path);
            var expected = JsonConvert.DeserializeObject<WeatherModel>(json);

            Assert.NotNull(result.Value.ResponseBody);
            Assert.NotNull(expected);
            Assert.IsType<WeatherModel>(result.Value.ResponseBody);
            Assert.Equal(expected.Main.TempC, result.Value.ResponseBody.Main.TempC);
            Assert.Equal(expected.Main.Temp, result.Value.ResponseBody.Main.Temp);
            Assert.Equal(expected.Name, result.Value.ResponseBody.Name);
            Assert.Equal(expected.Sys.Country, result.Value.ResponseBody.Sys.Country);
        }

        [Theory]
        [InlineData(3083426)]
        [InlineData(7531002)]
        [InlineData(2820565)]
        [InlineData(2820582)]

        public async Task Weather_Controller_Should_Return_Weather_By_City_ID(int id)
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
            WeatherController controller = new WeatherController(weatherService);

            var result = await controller.GetCurrentWeatherByCityID(id);
            Assert.NotNull(result.Value.ResponseBody);
            Assert.IsType<WeatherModel>(result.Value.ResponseBody);
        }

        [Theory]
        [InlineData(9.47558, 48.154549)]
        [InlineData(27.83333, 45.716671)]
        [InlineData(21.37611, 45.779442)]
        [InlineData(9.46917, 54.577782)]
        [InlineData(23.14275, 53.797421)]
        public async Task Weather_Controller_Should_Return_Weather_By_City_Coord(float lon, float lat)
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
            WeatherController controller = new WeatherController(weatherService);

            var result = await controller.GetCurrentWeatherByCityCoord(new API.Models.WeatherDetails.Coord 
            {
                Lat = lat,
                Lon = lon
             });

            Assert.NotNull(result.Value.ResponseBody);
            Assert.IsType<WeatherModel>(result.Value.ResponseBody);
        }

        [Fact]
        public async Task Weather_Controller_Should_Throw_ArgumentNullException()
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
            WeatherController controller = new WeatherController(weatherService);

            await Assert.ThrowsAsync<ArgumentNullException>(async ()=>
            {
                await controller.GetCurrentWeatherByCityCoord(null);
            });
        }

        /// this is helper method to update json file that contains weather details for testing weather controller
        async Task updateWeatherJson(string path, string city)
        {
            ApplicationData applicationData = new ApplicationData
            {
                OWMUrl = "https://api.openweathermap.org/data/2.5/weather?",
                OWMApiKey = "f1b58a47aa129daa6330e7280a88e7b5"
            };

            var mockFactory = new Mock<IHttpClientFactory>();
            mockFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(new HttpClient());
            IHttpClientFactory factory = mockFactory.Object;
            IClientService clientService = new ClientService(factory);
            IWeatherService weatherService = new WeatherService(clientService, Options.Create<ApplicationData>(applicationData));

            var weather = await weatherService.GetWeatherByCityNameAsync(city);
            var json = JsonConvert.SerializeObject(weather);

            File.WriteAllText(path,json);
        }

    }

}
