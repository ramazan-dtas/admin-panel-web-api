using skolesystem.Models;

// Service for business logic
public interface IBrugerService
{
    Task<Bruger> GetBrugerById(int id);
    Task<IEnumerable<Bruger>> GetAllBrugers();
    Task<IEnumerable<Bruger>> GetDeletedBrugers();
    Task AddBruger(Bruger bruger);
    Task UpdateBruger(Bruger bruger);
    Task SoftDeleteBruger(int id);
}

public class BrugerService : IBrugerService
{
    private readonly IBrugerRepository _brugerRepository;

    public BrugerService(IBrugerRepository brugerRepository)
    {
        _brugerRepository = brugerRepository;
    }

    public async Task<Bruger> GetBrugerById(int id)
    {
        return await _brugerRepository.GetById(id);
    }

    public async Task<IEnumerable<Bruger>> GetAllBrugers()
    {
        return await _brugerRepository.GetAll();
    }

    public async Task<IEnumerable<Bruger>> GetDeletedBrugers()
    {
        return await _brugerRepository.GetDeletedBrugers();
    }

    public async Task AddBruger(Bruger bruger)
    {
        await _brugerRepository.AddBruger(bruger);
    }

    public async Task UpdateBruger(Bruger bruger)
    {
        await _brugerRepository.UpdateBruger(bruger);
    }

    public async Task SoftDeleteBruger(int id)
    {
        await _brugerRepository.SoftDeleteBruger(id);
    }
}
