using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherApp.API.Services
{
    public class ClientService : IClientService
    {

        private readonly IHttpClientFactory _clientFactory;

        public ClientService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<T> GetAsync<T>(string url)
        {
            var client = _clientFactory.CreateClient();

            var response = await client.GetAsync(url);

            if(response.StatusCode == System.Net.HttpStatusCode.NotFound || response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                throw new InvalidOperationException("Invalid url");
            }

            var responseContent = await response.Content.ReadAsStringAsync();

            var model = JsonConvert.DeserializeObject(responseContent, typeof(T));

            return (T)model;
        }

        public Task<T> PostAsync<T>(object body)
        {
            throw new System.NotImplementedException();
        }
    }
}
