using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using skolesystem.Data;
using skolesystem.Models;

public interface IUser_informationRepository
{
    User_information GetById(int id);
    IEnumerable<User_information> GetAll();
    void Add(User_information bruger);
    void Update(User_information bruger);
    void Delete(int id);
}

public class User_informationRepository : IUser_informationRepository
{
    private readonly User_informationDbContext _dbContext;

    public User_informationRepository(User_informationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public User_information GetById(int id)
    {
        return _dbContext.User_information.Find(id);
    }

    public IEnumerable<User_information> GetAll()
    {
        return _dbContext.User_information.ToList();
    }

    public void Add(User_information bruger)
    {
        _dbContext.User_information.Add(bruger);
        _dbContext.SaveChanges();
    }

    public void Update(User_information bruger)
    {
        _dbContext.Entry(bruger).State = EntityState.Modified;
        _dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        var brugerToDelete = _dbContext.User_information.Find(id);

        if (brugerToDelete != null)
        {
            _dbContext.User_information.Remove(brugerToDelete);
            _dbContext.SaveChanges();
        }
    }
}
