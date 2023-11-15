using Microsoft.EntityFrameworkCore;
using skolesystem.Models;

namespace skolesystem.Data
{
    public class User_informationDbContext : DbContext
    {
        public User_informationDbContext(DbContextOptions<User_informationDbContext> options) : base(options) 
        {

        }

        public DbSet<User_information> User_information { get; set; }
    }
}
