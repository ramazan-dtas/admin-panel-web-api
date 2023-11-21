using Microsoft.EntityFrameworkCore;
using skolesystem.Models;

namespace skolesystem.Data
{
    public class AbsenceDbContext : DbContext
    {
        public DbSet<Absence> Absence { get; set; }
        public DbSet<Users> users { get; set; }
        public DbSet<Classe> classe { get; set; }


        public AbsenceDbContext(DbContextOptions<AbsenceDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Absence>().HasKey(a => a.absence_id);


            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Absence>()
            .HasOne(s => s.Classe)
            .WithMany(c => c.absences)
            .HasForeignKey(s => s.class_id);

            modelBuilder.Entity<Absence>()
           .HasOne(s => s.User)
           .WithMany(c => c.absences)
           .HasForeignKey(s => s.user_id);
        }
    }
}
