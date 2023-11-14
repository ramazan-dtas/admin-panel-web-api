using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using skolesystem.Data;
using skolesystem.DTOs; 
using skolesystem.Models;
using skolesystem.Service;

namespace skolesystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UsersDbContext _context;
        private readonly IUsersService _usersService;

        public UserController(UsersDbContext context, IUsersService usersService)
        {
            _context = context;
            _usersService = usersService;
        }

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
                    email_confirmed = user.email_confirmed,
                    lockout_enabled = user.lockout_enabled,
                    phone_confirmed = user.phone_confirmed,
                    twofactor_enabled = user.twofactor_enabled,
                    try_failed_count = user.try_failed_count,
                    lockout_end = user.lockout_end,
                    user_information_id = user.user_information_id,
                    is_deleted = user.is_deleted
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
                email_confirmed = user.email_confirmed,
                lockout_enabled = user.lockout_enabled,
                phone_confirmed = user.phone_confirmed,
                twofactor_enabled = user.twofactor_enabled,
                try_failed_count = user.try_failed_count,
                lockout_end = user.lockout_end,
                user_information_id = user.user_information_id,
                is_deleted = user.is_deleted
            };

            return Ok(userDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateUser(UserCreateDto userDto)
        {
            var user = new Users
            {
                surname = userDto.surname,
                email = userDto.email,
                password_hash = userDto.password_hash

            };

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
    }
}

