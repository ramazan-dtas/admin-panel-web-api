using skolesystem.Models;
using skolesystem.Repository;

namespace skolesystem.Service
{
    //Service for business logic
    public interface IUsersService
    {
        Task<Users> GetUserById(int id);
        Task<IEnumerable<Users>> GetAllUsers();
        Task<IEnumerable<Users>> GetDeletedUsers();
        Task AddUser(Users user);
        Task UpdateUser(Users user);
        Task SoftDeleteUser(int id);
    }
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<Users> GetUserById(int id)
        {
            return await _usersRepository.GetById(id);
        }

        public async Task<IEnumerable<Users>> GetAllUsers()
        {
            return await _usersRepository.GetAll();
        }

        public async Task<IEnumerable<Users>> GetDeletedUsers()
        {
            return await _usersRepository.GetDeletedUsers();
        }

        public async Task AddUser(Users user)
        {
            await _usersRepository.AddUser(user);
        }

        public async Task UpdateUser(Users user)
        {
            await _usersRepository.UpdateUser(user);
        }

        public async Task SoftDeleteUser(int id)
        {
            await _usersRepository.SoftDeleteUser(id);
        }
    }

}
