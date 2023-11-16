using Microsoft.EntityFrameworkCore;
using skolesystem.Data;
using skolesystem.Models;

namespace skolesystem.Repository
{
    //Repository for data access
    public interface IUsersRepository
    {
        Task<Users> GetById(int id);
        Task<IEnumerable<Users>> GetAll();
        Task<IEnumerable<Users>> GetDeletedUsers();
        Task AddUser(Users user);
        Task UpdateUser(Users user);
        Task SoftDeleteUser(int id);
        Task<Users> GetBySurname(string surname);
    }


    public class UsersRepository : IUsersRepository
    {
        private readonly UsersDbContext _context;

        public UsersRepository(UsersDbContext context)
        {
            _context = context;
        }

        public async Task<Users> GetBySurname(string surname)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.surname == surname);
        }

        public async Task<Users> GetById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<Users>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<IEnumerable<Users>> GetDeletedUsers()
        {
            return await _context.Users.Where(u => u.is_deleted).ToListAsync();
        }

        public async Task AddUser(Users user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUser(Users user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteUser(int id)
        {
            var userToDelete = await _context.Users.FindAsync(id);

            if (userToDelete != null)
            {
                userToDelete.is_deleted = true;
                _context.Entry(userToDelete).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }
    }

}
