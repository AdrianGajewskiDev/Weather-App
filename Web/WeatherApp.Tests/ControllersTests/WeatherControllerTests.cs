using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
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

        /// <summary>
        /// this is helper method to update json file that contains weather details for testing weather controller
        /// </summary>
        /// <param name="path"></param>
        /// <param name="city"></param>
        /// <returns></returns>
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
            var json = JsonConvert.SerializeObject(weather.ResponseBody);

            File.WriteAllText(path,json);
        }

    }

}
