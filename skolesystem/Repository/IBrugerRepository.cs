using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using skolesystem.Data;
using skolesystem.Models;

// Repository for data access
public interface IBrugerRepository
{
    Task<Bruger> GetById(int id);
    Task<IEnumerable<Bruger>> GetAll();
    Task<IEnumerable<Bruger>> GetDeletedBrugers();
    Task AddBruger(Bruger bruger);
    Task UpdateBruger(Bruger bruger);
    Task SoftDeleteBruger(int id);
}

public class BrugerRepository : IBrugerRepository
{
    private readonly BrugerDbContext _context;

    public BrugerRepository(BrugerDbContext context)
    {
        _context = context;
    }

    public async Task<Bruger> GetById(int id)
    {
        return await _context.Bruger.FindAsync(id);
    }

    public async Task<IEnumerable<Bruger>> GetAll()
    {
        return await _context.Bruger.ToListAsync();
    }

    public async Task<IEnumerable<Bruger>> GetDeletedBrugers()
    {
        return await _context.Bruger.Where(b => b.is_deleted).ToListAsync();
    }

    public async Task AddBruger(Bruger bruger)
    {
        _context.Bruger.Add(bruger);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateBruger(Bruger bruger)
    {
        _context.Entry(bruger).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task SoftDeleteBruger(int id)
    {
        var brugerToDelete = await _context.Bruger.FindAsync(id);

        if (brugerToDelete != null)
        {
            brugerToDelete.is_deleted = true;
            _context.Entry(brugerToDelete).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}


