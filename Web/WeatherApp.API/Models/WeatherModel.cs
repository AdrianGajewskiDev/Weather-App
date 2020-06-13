using WeatherApp.API.Models.WeatherDetails;

namespace WeatherApp.API.Models
{
    public class WeatherModel
    {
        public Weather[] Weather { get; set; }
        public Main Main{ get; set; }

    }
}
