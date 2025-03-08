using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherArchives.Models
{
    public class WeatherRecord
    {
        public int Id { get; set; }
        [Column(TypeName = "timestamp")]
        public DateTime Date { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public double Td { get; set; }
        public int AirPressure { get; set; }
        public string WindDirection  { get; set; }
        public int WindSpeed { get; set; }
        public int Clouds  { get; set; }
        public int H { get; set; }
        public int VV { get; set; }
        public string? Description { get; set; } 
    }
}
