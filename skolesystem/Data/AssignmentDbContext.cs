using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using skolesystem.Models;

namespace skolesystem.Data
{
	public class AssignmentDbContext : DbContext
	{
        public DbSet<Classe> Classe { get; set; }
        public DbSet<Assignment> Assignments { get; set; }

        public AssignmentDbContext(DbContextOptions<AssignmentDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Assignment>()
            .HasOne(s => s.Classe)
            .WithMany(c => c.assignments)
            .HasForeignKey(s => s.class_id);
        }
    }
}

