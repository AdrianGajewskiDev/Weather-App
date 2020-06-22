namespace WeatherApp.API.Models.WeatherDetails
{
    public class Coord
    {
        public float Lon { get; set; }
        public float Lat{ get; set; }

        public override string ToString()
        {
            return $"lat: {Lat}, lon: {Lon}";
        }
    }
}
