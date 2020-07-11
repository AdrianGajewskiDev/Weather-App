using System.Collections.Generic;

namespace WeatherApp.API.Models
{
    public class LongWeatherForecastModel
    {
        public IEnumerable<LongWeatherForecastListItemModel> List { get; set; }
        public City City { get; set; }
    }
}
