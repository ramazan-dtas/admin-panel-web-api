using skolesystem.Authorization;
using skolesystem.DTOs;
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
        Task<LoginResponse> Authenticate(LoginRequest login);
    }
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IJwtUtils _jwtUtils;

        public UsersService(IUsersRepository usersRepository, IJwtUtils jwtUtils)
        {
            _usersRepository = usersRepository;
            _jwtUtils = jwtUtils;
        }

        public async Task<LoginResponse> Authenticate(LoginRequest login)
        {
            Users user = await _usersRepository.GetBySurname(login.surname);
            if (user == null)
            {
                return null;
            }

            if (user.password_hash == login.password_hash)
            {
                LoginResponse response = new()
                {
                    user_id = user.user_id,
                    surname = user.surname,
                    email = user.email,
                    role_id = user.role_id,
                    is_deleted = user.is_deleted,
                    Token = _jwtUtils.GenerateJwtToken(user)
                };
                return response;
            }

            return null;
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
