using System.Net;

namespace WeatherApp.API.Models
{
    
    
    public class ApiResponse<T>
    {
        public  T ResponseBody { get; set; }
        public HttpStatusCode StatusCode { get; set; }

    }
}
