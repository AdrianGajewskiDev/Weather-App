using System.Threading.Tasks;

namespace WeatherApp.API.Services
{
    public interface IClientService
    {
        Task<T> GetAsync<T>(string url);
        Task<T> PostAsync<T>(object body);
    }
}
