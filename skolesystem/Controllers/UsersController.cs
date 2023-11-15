using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using skolesystem.Data;
using skolesystem.DTOs; 
using skolesystem.Models;

namespace skolesystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UsersDbContext _context; 

        public UserController(UsersDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<UserReadDto>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            // Map your User entities to UserReadDto using AutoMapper or manual mapping
            // For simplicity, manual mapping is shown here
            var userDtos = new List<UserReadDto>();
            foreach (var user in users)
            {
                userDtos.Add(new UserReadDto
                {
                    user_id = user.user_id,
                    surname = user.surname,
                    email = user.email,
                    password_hash = user.password_hash,

                    is_deleted = user.is_deleted

                }) ;
            }
            return userDtos;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserReadDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var userDto = new UserReadDto
            {
                user_id = user.user_id,
                surname = user.surname,
                email = user.email
                // Map other fields as needed
            };

            return Ok(userDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateUser(UserCreateDto userDto)
        {
            // Map UserCreateDto to User entity using AutoMapper or manual mapping
            // For simplicity, manual mapping is shown here
            var user = new Users
            {
                surname = userDto.surname,
                email = userDto.email,
                password_hash = userDto.password_hash
                // Map other fields as needed
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserById), new { id = user.user_id }, userDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserUpdateDto userDto)
        {
            var userToUpdate = await _context.Users.FindAsync(id);

            if (userToUpdate == null)
            {
                return NotFound();
            }

            // Map UserUpdateDto to User entity using AutoMapper or manual mapping
            // For simplicity, manual mapping is shown here
            userToUpdate.surname = userDto.surname;
            userToUpdate.email = userDto.email;
            // Map other fields as needed

            _context.Entry(userToUpdate).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var userToDelete = await _context.Users.FindAsync(id);
            if (userToDelete == null) return NotFound();

            // Soft delete by setting is_deleted to true
            userToDelete.is_deleted = true;

            // Update the entity in the database
            _context.Entry(userToDelete).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
