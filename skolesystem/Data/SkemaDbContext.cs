using Microsoft.EntityFrameworkCore;
using skolesystem.Models;

namespace skolesystem.Data
{
    public class SkemaDbContext : DbContext
    {
        public SkemaDbContext(DbContextOptions<SkemaDbContext> options) : base(options)
        {

        }

        public DbSet<Skema> Skema { get; set; }
    }
}
