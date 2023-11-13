using Microsoft.EntityFrameworkCore;
using skolesystem.Models;

namespace skolesystem.Data
{
    public class UsersDbContext : DbContext
    {
       
         public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
         {

         }

         public DbSet<Users> Users { get; set; }
    }
}
