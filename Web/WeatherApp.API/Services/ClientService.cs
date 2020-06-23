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

        public async Task<ApiResponse<T>> GetAsync<T>(string url)
        {
            var client = _clientFactory.CreateClient();

            var response = await client.GetAsync(url);

            if(response.StatusCode == System.Net.HttpStatusCode.NotFound || response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                Log.Information($"Cannot find weather for {url}");
                return new ApiResponse<T>
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    ResponseBody = default
                };
            }

            var responseContent = await response.Content.ReadAsStringAsync();

            var responseBody = JsonConvert.DeserializeObject(responseContent, typeof(T));

            return new ApiResponse<T> 
            {
                ResponseBody = (T)(responseBody),
                StatusCode = System.Net.HttpStatusCode.OK 
            };
        }

        public Task<ApiResponse<T>> PostAsync<T>(object body)
        {
            throw new System.NotImplementedException();
        }
    }
}
