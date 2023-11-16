using AutoMapper;
using skolesystem.DTOs;
using skolesystem.Models;
using skolesystem.Repository;
using System.Threading.Tasks;

namespace skolesystem.Service
{
    //Service for business logic
    //public interface IUsersService
    //{
    //    Task<Users> GetUserById(int id);
    //    Task<IEnumerable<Users>> GetAllUsers();
    //    Task<IEnumerable<Users>> GetDeletedUsers();
    //    Task AddUser(Users user);
    //    Task UpdateUser(Users user);
    //    Task SoftDeleteUser(int id);
    //}
    public interface IUsersService
    {
        Task<UserReadDto> GetUserById(int id);
        Task<IEnumerable<UserReadDto>> GetAllUsers();
        Task<IEnumerable<UserReadDto>> GetDeletedUsers();
        Task AddUser(UserCreateDto user);
        Task UpdateUser(int id, UserUpdateDto user);
        Task SoftDeleteUser(int id);
    }

    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public UsersService(IUsersRepository usersRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        public async Task<UserReadDto> GetUserById(int id)
        {
            var user = await _usersRepository.GetById(id);
            return _mapper.Map<UserReadDto>(user);
        }

        public async Task<IEnumerable<UserReadDto>> GetAllUsers()
        {
            var users = await _usersRepository.GetAll();
            return _mapper.Map<IEnumerable<UserReadDto>>(users);
        }

        public async Task<IEnumerable<UserReadDto>> GetDeletedUsers()
        {
            var deletedUsers = await _usersRepository.GetDeletedUsers();
            return _mapper.Map<IEnumerable<UserReadDto>>(deletedUsers);
        }

        public async Task AddUser(UserCreateDto user)
        {
            var userEntity = _mapper.Map<Users>(user);
            await _usersRepository.AddUser(userEntity);
        }

        public async Task UpdateUser(int id, UserUpdateDto userDto)
        {
            var existingUser = await _usersRepository.GetById(id);

            if (existingUser == null)
            {
                // Handle the case where the user with the given id is not found.
                // You can return an error response or throw an exception.
                // For now, let's assume you return NotFound.
                //return NotFound();
            }

            // Update the properties of existingUser based on userDto
            existingUser.surname = userDto.surname;
            existingUser.email = userDto.email;

            // Save the changes
            await _usersRepository.UpdateUser(existingUser);
        }

        public async Task SoftDeleteUser(int id)
        {
            await _usersRepository.SoftDeleteUser(id);
        }
    }


}
