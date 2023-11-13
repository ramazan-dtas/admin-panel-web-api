using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using skolesystem.Data;
using skolesystem.Models;

public interface IBrugerRepository
{
    Bruger GetById(int id);
    IEnumerable<Bruger> GetAll();
    void Add(Bruger bruger);
    void Update(Bruger bruger);
    void Delete(int id);
}

public class BrugerRepository : IBrugerRepository
{
    private readonly BrugerDbContext _dbContext;

    public BrugerRepository(BrugerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Bruger GetById(int id)
    {
        return _dbContext.Bruger.Find(id);
    }

    public IEnumerable<Bruger> GetAll()
    {
        return _dbContext.Bruger.ToList();
    }

    public void Add(Bruger bruger)
    {
        _dbContext.Bruger.Add(bruger);
        _dbContext.SaveChanges();
    }

    public void Update(Bruger bruger)
    {
        _dbContext.Entry(bruger).State = EntityState.Modified;
        _dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        var brugerToDelete = _dbContext.Bruger.Find(id);

        if (brugerToDelete != null)
        {
            _dbContext.Bruger.Remove(brugerToDelete);
            _dbContext.SaveChanges();
        }
    }
}
