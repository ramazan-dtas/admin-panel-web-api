using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using skolesystem.Data;
using skolesystem.DTOs; 
using skolesystem.Models;
using skolesystem.Service;
using skolesystem.Authorization;
using skolesystem.Migrations.UsersDb;
using Users = skolesystem.Models.Users;
using skolesystem.Repository;
using BCrypt.Net;

namespace skolesystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UsersDbContext _context;
        private readonly IUsersService _usersService;
        private readonly IUsersRepository _userRepository;
        private readonly IJwtUtils _jwtUtils;


        public UserController(UsersDbContext context, IUsersRepository usersRepository,IUsersService usersService, IJwtUtils jwtUtils)
        {
            _context = context;
            _usersService = usersService;
            _userRepository = usersRepository;
            _jwtUtils = jwtUtils;
        }

        

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(LoginRequest login)
        {
            try
            {
                Users user = await _userRepository.GetBySurname(login.surname);

                if (user == null)
                {
                    return Unauthorized();
                }

                if (user.password_hash == login.password_hash || BCrypt.Net.BCrypt.Verify(login.password_hash, user.password_hash))//()
                {

                    return Ok(new LoginResponse
                    {

                        user_id = user.user_id,
                        surname = user.surname,
                        email = user.email,
                        is_deleted = user.is_deleted,
                        role_id = user.role_id,
                        Token = _jwtUtils.GenerateJwtToken(user)

                    });
                }

                return Unauthorized();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [Authorize(1)]
        [HttpGet]
        public async Task<IEnumerable<UserReadDto>> GetUsers()
        {
            var users = await _usersService.GetAllUsers();
            var userDtos = new List<UserReadDto>();
            foreach (var user in users)
            {
                userDtos.Add(new UserReadDto
                {
                    user_id = user.user_id,
                    surname = user.surname,
                    email = user.email,
                    password_hash = user.password_hash,
                    is_deleted = user.is_deleted,
                    role_id = user.role_id
                });
            }
            return userDtos;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserReadDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _usersService.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            var userDto = new UserReadDto
            {
                user_id = user.user_id,
                surname = user.surname,
                email = user.email,
                password_hash = user.password_hash,
                is_deleted = user.is_deleted,
                role_id = user.role_id
            };

            return Ok(userDto);
        }

        /*public string HashPassword(string password)
        {
            // Generate a random salt
            string salt = BCrypt.Net.BCrypt.GenerateSalt(12); // 12 is the recommended saltWorkFactor

            // Hash the password with the salt
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);

            return hashedPassword;
        }*/

        [Authorize(1)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateUser(UserCreateDto userDto)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt(12);
            var user = new Users
            {
                surname = userDto.surname,
                email = userDto.email,
                password_hash = userDto.password_hash,
                role_id = userDto.role_id

            };
            user.password_hash = BCrypt.Net.BCrypt.HashPassword(userDto.password_hash, salt);


            await _usersService.AddUser(user);

            return CreatedAtAction(nameof(GetUserById), new { id = user.user_id }, userDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserUpdateDto userDto)
        {
            var existingUser = await _usersService.GetUserById(id);

            if (existingUser == null)
            {
                return NotFound();
            }
 
            existingUser.surname = userDto.surname;
            existingUser.email = userDto.email;
            existingUser.role_id = userDto.role_id;




            await _usersService.UpdateUser(existingUser);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var userToDelete = await _usersService.GetUserById(id);

            if (userToDelete == null)
            {
                return NotFound();
            }

            await _usersService.SoftDeleteUser(id);

            return NoContent();
        }



        public static UserReadDto MapUserTouserResponse(Users user)
        {
            return new UserReadDto
            {
                user_id = user.user_id,
                surname = user.surname,
                email = user.email,
                role_id = user.role_id
            };
        }
    }
}

