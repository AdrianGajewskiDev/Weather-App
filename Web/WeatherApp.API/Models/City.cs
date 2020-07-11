using WeatherApp.API.Models.WeatherDetails;

namespace WeatherApp.API.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Coord Coord { get; set; }
        public string Country { get; set; }
        public double Population { get; set; }
    }
}
