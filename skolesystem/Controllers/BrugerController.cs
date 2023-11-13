using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using skolesystem.Data;
using skolesystem.DTOs;
using skolesystem.Models;

namespace skolesystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrugerController : ControllerBase
    {
        private readonly BrugerDbContext _context;
        public BrugerController(BrugerDbContext context) => _context = context;

        [HttpGet]
        public async Task<IEnumerable<Bruger>> Get() 
        {
            return await _context.Bruger.ToListAsync();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Bruger), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id) 
        {
            var bruger = await _context.Bruger.FindAsync(id);
            return bruger == null ? NotFound() : Ok(bruger);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(Bruger bruger)
        {
            await _context.Bruger.AddAsync(bruger);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = bruger.user_information_id }, bruger);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, BrugerUpdateDto brugerDto)
        {
            if (id != brugerDto.user_information_id)
            {
                return BadRequest();
            }

            var brugerToUpdate = await _context.Bruger.FindAsync(id);

            if (brugerToUpdate == null)
            {
                return NotFound();
            }

            // Map properties from DTO to the entity
            brugerToUpdate.name = brugerDto.name;
            brugerToUpdate.last_name = brugerDto.last_name;
            brugerToUpdate.phone = brugerDto.phone;
            brugerToUpdate.date_of_birth = brugerDto.date_of_birth;
            brugerToUpdate.address = brugerDto.address;
            brugerToUpdate.is_deleted = brugerDto.is_deleted;
            

            _context.Entry(brugerToUpdate).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }





        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var brugerToDelete = await _context.Bruger.FindAsync(id);
            if (brugerToDelete == null) return NotFound();

            // Soft delete by setting is_deleted to true
            brugerToDelete.is_deleted = true;

            // Update the entity in the database
            _context.Entry(brugerToDelete).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
