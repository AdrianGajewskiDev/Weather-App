﻿using System.ComponentModel.DataAnnotations;

namespace WeatherApp.API.Models.Entities
{
    public class Notification
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; }

        [Required]
        [MaxLength(length: 50)]
        public string RequestedCityName { get; set; }
    }
}
