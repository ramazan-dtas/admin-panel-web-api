using Microsoft.EntityFrameworkCore;
using skolesystem.Models;

namespace skolesystem.Data
{
    public class AbsenceDbContext : DbContext
    {
        public DbSet<Absence> Absences { get; set; }

        public AbsenceDbContext(DbContextOptions<AbsenceDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Absence>().HasKey(a => a.absence_id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
