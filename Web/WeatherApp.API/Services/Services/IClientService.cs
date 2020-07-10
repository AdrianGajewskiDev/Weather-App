using System.Threading.Tasks;
using WeatherApp.API.Models;

namespace WeatherApp.API.Services
{
    public interface IClientService
    {
        Task<T> GetAsync<T>(string url) where T : class,new();
        Task<T> PostAsync<T>(object body);
    }
}
