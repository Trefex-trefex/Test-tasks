using Microsoft.EntityFrameworkCore;
using WeatherArchives.Models;

namespace WeatherArchives.Data
{
    public class WeatherContext : DbContext
    {
        public WeatherContext(DbContextOptions<WeatherContext> options)
            : base(options)
        { }

        public DbSet<WeatherRecord> WeatherRecords { get; set; }
    }
}
