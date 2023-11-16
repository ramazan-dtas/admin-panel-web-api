using Microsoft.EntityFrameworkCore;
using skolesystem.Models;
namespace skolesystem.Data
{
	public class SubjectsDbContext : DbContext
	{
        public SubjectsDbContext(DbContextOptions<SubjectsDbContext> options) : base(options)
        {

        }

        public DbSet<Subjects> Subjects { get; set; }
    }
}

