using System;
using System.ComponentModel.DataAnnotations;

namespace WeatherArchives.Models
{
    public class WeatherRecord
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public double Temperature { get; set; }
        public int Humidity { get; set; }
        public double WindSpeed { get; set; }
        public string? Description { get; set; } 
    }
}
