using skolesystem.Models;

public interface IBrugerService
{
    Bruger GetById(int id);
    IEnumerable<Bruger> GetAll();
    void Add(Bruger bruger);
    void Update(Bruger bruger);
    void Delete(int id);
}

// BrugerService implementation
public class BrugerService : IBrugerService
{
    private readonly IBrugerRepository _brugerRepository;

    public BrugerService(IBrugerRepository brugerRepository)
    {
        _brugerRepository = brugerRepository;
    }

    public Bruger GetById(int id)
    {
        return _brugerRepository.GetById(id);
    }

    public IEnumerable<Bruger> GetAll()
    {
        return _brugerRepository.GetAll();
    }

    public void Add(Bruger bruger)
    {
        // Add any business logic/validation before calling the repository
        _brugerRepository.Add(bruger);
    }

    public void Update(Bruger bruger)
    {
        // Add any business logic/validation before calling the repository
        _brugerRepository.Update(bruger);
    }

    public void Delete(int id)
    {
        // Add any business logic/validation before calling the repository
        _brugerRepository.Delete(id);
    }
}
