using Microsoft.EntityFrameworkCore;
using skolesystem.Models;

namespace skolesystem.Data
{
    public class BrugerDbContext : DbContext
    {
        public BrugerDbContext(DbContextOptions<BrugerDbContext> options) : base(options) 
        {

        }

        public DbSet<Bruger> Bruger { get; set; }
    }
}
