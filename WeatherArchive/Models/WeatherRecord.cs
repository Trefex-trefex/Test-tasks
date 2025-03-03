using System;

namespace WeatherArchive.Models
{
    public class WeatherRecord
    {
        public int Id { get; set; } 
        public DateTime Date { get; set; }
        public float Temperature { get; set; }
    }
}
