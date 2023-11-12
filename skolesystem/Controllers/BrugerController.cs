using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using skolesystem.Data;
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, Bruger bruger)
        {
            if (id != bruger.user_information_id) return BadRequest();
            _context.Entry(bruger).State = EntityState.Modified;
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

            _context.Bruger.Remove(brugerToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
