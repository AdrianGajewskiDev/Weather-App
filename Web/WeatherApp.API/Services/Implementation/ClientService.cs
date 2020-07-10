using Newtonsoft.Json;
using Serilog;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherApp.API.Models;

namespace WeatherApp.API.Services
{
    public class ClientService : IClientService
    {

        private readonly IHttpClientFactory _clientFactory;

        public ClientService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<T> GetAsync<T>(string url) where T: class, new()
        {
            var client = _clientFactory.CreateClient();

            var response = await client.GetAsync(url);

            if(response.StatusCode == System.Net.HttpStatusCode.NotFound || response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                Log.Information($"Cannot find weather for {url}");
                return default;
            }

            string responseContent = await response.Content.ReadAsStringAsync();


            if (response.Content.Headers.ContentType.MediaType != "application/json")
            {
                throw new InvalidOperationException("The response content type is not JSON");
            }

            var resJSON = JsonConvert.DeserializeObject(responseContent, typeof(T));

            return (T)resJSON;
        }

        public Task<T> PostAsync<T>(object body)
        {
            throw new System.NotImplementedException();
        }
    }
}
