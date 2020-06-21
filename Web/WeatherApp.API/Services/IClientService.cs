using System.Threading.Tasks;
using WeatherApp.API.Models;

namespace WeatherApp.API.Services
{
    public interface IClientService
    {
        Task<ApiResponse<T>> GetAsync<T>(string url);
        Task<ApiResponse<T>> PostAsync<T>(object body);
    }
}
