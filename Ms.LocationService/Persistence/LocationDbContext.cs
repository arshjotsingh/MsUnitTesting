using Microsoft.EntityFrameworkCore;
using Ms.LocationService.Models;

namespace Ms.LocationService.Persistence
{
    public class LocationDbContext : DbContext
    {
        public LocationDbContext(DbContextOptions<LocationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Location> Locations { get; set; }
    }
}
