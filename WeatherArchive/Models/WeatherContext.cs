using Microsoft.EntityFrameworkCore;

namespace WeatherArchive.Models
{
    public class WeatherContext : DbContext
    {
        public WeatherContext(DbContextOptions<WeatherContext> options) : base(options) { }

        public DbSet<WeatherRecord> WeatherRecords { get; set; }    
    }
}
