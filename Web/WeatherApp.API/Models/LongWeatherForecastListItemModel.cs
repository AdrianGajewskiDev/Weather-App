using System;
using WeatherApp.API.Models.WeatherDetails;

namespace WeatherApp.API.Models
{
    public class LongWeatherForecastListItemModel
    {
        public Main Main { get; set; }
        public Wind Wind { get; set; }
        public Weather[] Weather { get; set; }
        public Clouds Clouds { get; set; }

        public string Dt_txt { get; set; }
        public DateTime WeatherForecastDateTime { get => DateTime.Parse(Dt_txt); }
    }
}
