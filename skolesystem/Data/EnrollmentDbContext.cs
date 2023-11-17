using System;
using Microsoft.EntityFrameworkCore;
using skolesystem.Models;

namespace skolesystem.Data
{
	public class EnrollmentDbContext : DbContext
	{
        public DbSet<Enrollments> enrollments { get; set; }
        public DbSet<Users> users { get; set; }
        public DbSet<Classe> classe { get; set; }
        public EnrollmentDbContext(DbContextOptions<EnrollmentDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enrollments>()
            .HasOne(s => s.Classe)
            .WithMany(c => c.enrollments)
            .HasForeignKey(s => s.class_id);

            modelBuilder.Entity<Enrollments>()
           .HasOne(s => s.User)
           .WithMany(c => c.enrollments)
           .HasForeignKey(s => s.user_id);
        }
    }
}

