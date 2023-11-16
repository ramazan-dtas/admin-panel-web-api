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

            if (users == null)
            {
                // Handle the case where the service returns null (e.g., return an empty collection)
                return Enumerable.Empty<UserReadDto>();
            }

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateUser(UserCreateDto userDto)
        {
            try
            {
                var user = new Users
                {
                    surname = userDto.surname,
                    email = userDto.email,
                    password_hash = userDto.password_hash
                };

                _usersService.AddUser(userDto);

                return CreatedAtAction(nameof(GetUserById), new { id = user.user_id }, userDto);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUser(int id, UserUpdateDto userDto)
        {
            await _usersService.UpdateUser(id, userDto);
            return Ok();
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

