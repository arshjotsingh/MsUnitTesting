using Microsoft.EntityFrameworkCore;
using Ms.TeamService.Models;

namespace Ms.TeamService.Persistence
{
    public class TeamDbContext : DbContext
    {
        public TeamDbContext(DbContextOptions<TeamDbContext> options)
            :base(options)
        {
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Member> Members { get; set; }
    }
}
