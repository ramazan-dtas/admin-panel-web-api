using skolesystem.Models;

public interface IUser_informationService
{
    User_information GetById(int id);
    IEnumerable<User_information> GetAll();
    void Add(User_information bruger);
    void Update(User_information bruger);
    void Delete(int id);
}

// BrugerService implementation
public class User_informationService : IUser_informationService
{
    private readonly IUser_informationRepository _brugerRepository;

    public User_informationService(IUser_informationRepository brugerRepository)
    {
        _brugerRepository = brugerRepository;
    }

    public User_information GetById(int id)
    {
        return _brugerRepository.GetById(id);
    }

    public IEnumerable<User_information> GetAll()
    {
        return _brugerRepository.GetAll();
    }

    public void Add(User_information bruger)
    {
        // Add any business logic/validation before calling the repository
        _brugerRepository.Add(bruger);
    }

    public void Update(User_information bruger)
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
