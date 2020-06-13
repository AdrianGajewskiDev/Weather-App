using System;

namespace WeatherApp.API.Models.WeatherDetails
{
    public class Sys
    {
        public string Country { get; set; }
        public double Sunrise { get; set; }
        public double Sunset { get; set; }

        public string SunsetTime 
        { 
            get
            {
                var date = (new DateTime(1970, 1, 1)).AddMilliseconds(Sunset * 1000);
                return date.ToShortTimeString();
            }
        }

        public string SunriseTime
        {
            get
            {
                var date = (new DateTime(1970, 1, 1)).AddMilliseconds(Sunrise * 1000);
                return date.ToShortTimeString();
            }
        }
    }
}
