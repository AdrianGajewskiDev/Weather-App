using System;

namespace WeatherApp.API.Models
{
    public class Main
    {
        public float Temp { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
        public float Temp_min { get; set; }
        public float Temp_max { get; set; }
        public int TempC { get => (int)Math.Round(Temp - 273.15); }
    }
}
