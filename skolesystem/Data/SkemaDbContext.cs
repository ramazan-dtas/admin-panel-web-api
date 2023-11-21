using Microsoft.EntityFrameworkCore;
using skolesystem.Models;

namespace skolesystem.Data
{
    public class SkemaDbContext : DbContext
    {
        public DbSet<Classe> Classe { get; set; }
        public DbSet<Skema> skema { get; set; }


        public SkemaDbContext(DbContextOptions<SkemaDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skema>()
            .HasOne(s => s.Classe)
            .WithMany(c => c.skemas)
            .HasForeignKey(s => s.class_id);
        }

    }
}
