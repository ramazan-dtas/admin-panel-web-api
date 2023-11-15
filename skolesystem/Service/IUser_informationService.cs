using skolesystem.Models;

// Service for business logic
public interface IUser_informationService
{
    Task<User_information> GetBrugerById(int id);
    Task<IEnumerable<User_information>> GetAllBrugers();
    Task<IEnumerable<User_information>> GetDeletedBrugers();
    Task AddBruger(User_information bruger);
    Task UpdateBruger(User_information bruger);
    Task SoftDeleteBruger(int id);
}

public class User_informationService : IUser_informationService
{
    private readonly IUser_informationRepository _brugerRepository;

    public User_informationService(IUser_informationRepository brugerRepository)
    {
        _brugerRepository = brugerRepository;
    }

    public async Task<User_information> GetBrugerById(int id)
    {
        return await _brugerRepository.GetById(id);
    }

    public async Task<IEnumerable<User_information>> GetAllBrugers()
    {
        return await _brugerRepository.GetAll();
    }


    public async Task<IEnumerable<User_information>> GetDeletedBrugers()
    {
        return await _brugerRepository.GetDeletedBrugers();
    }

    public async Task AddBruger(User_information bruger)
    {
        await _brugerRepository.AddBruger(bruger);
    }

    public async Task UpdateBruger(User_information bruger)
    {
        await _brugerRepository.UpdateBruger(bruger);
    }

    public async Task SoftDeleteBruger(int id)
    {
        await _brugerRepository.SoftDeleteBruger(id);
    }
}
