using System;
using Microsoft.EntityFrameworkCore;
using skolesystem.Models;

namespace skolesystem.Data
{
	public class ClasseDbContext : DbContext
	{
        public ClasseDbContext(DbContextOptions<ClasseDbContext> options) : base(options)
        {

        }

        public DbSet<Classe> Classe { get; set; }
    }
}

