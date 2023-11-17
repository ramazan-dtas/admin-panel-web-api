using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using skolesystem.Data;
using skolesystem.Models;

// Repository for data access
public interface IUser_informationRepository
{
    Task<User_information> GetById(int id);
    Task<IEnumerable<User_information>> GetAll();
    Task<IEnumerable<User_information>> GetAllStudents();
    Task<IEnumerable<User_information>> GetDeletedBrugers();
    Task AddBruger(User_information bruger);
    Task UpdateBruger(User_information bruger);
    Task SoftDeleteBruger(int id);
}

public class User_informationRepository : IUser_informationRepository
{
    private readonly User_informationDbContext _context;

    public User_informationRepository(User_informationDbContext context)
    {
        _context = context;
    }

    public async Task<User_information> GetById(int id)
    {
        return await _context.User_information.FindAsync(id);
    }

    public async Task<IEnumerable<User_information>> GetAll()
    {
        return await _context.User_information.ToListAsync();
    }

    public async Task<IEnumerable<User_information>> GetAllStudents()
    {
        return await _context.User_information.Where(b => b.is_deleted == false && b.user_id == 3 ).ToListAsync();
    }

    public async Task<IEnumerable<User_information>> GetDeletedBrugers()
    {
        return await _context.User_information.Where(b => b.is_deleted).ToListAsync();
    }

    public async Task AddBruger(User_information bruger)
    {
        _context.User_information.Add(bruger);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateBruger(User_information bruger)
    {
        _context.Entry(bruger).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task SoftDeleteBruger(int id)
    {
        var brugerToDelete = await _context.User_information.FindAsync(id);

        if (brugerToDelete != null)
        {
            brugerToDelete.is_deleted = true;
            _context.Entry(brugerToDelete).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}

