using System.ComponentModel.DataAnnotations;

namespace WeatherApp.API.Models.Request
{
    public class NotificationRequestModel
    {
        public int ID { get; set; }

        public string UserEmail { get; set; }

        public string RequestedCityName { get; set; }

    }
}
