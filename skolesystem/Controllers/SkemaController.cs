using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using skolesystem.Data;
using skolesystem.Models;

namespace skolesystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkemaController : ControllerBase
    {
        private readonly SkemaDbContext _context;

        public SkemaController(SkemaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Skema>> Get()
        {
            return (IEnumerable<Skema>)await _context.Skema.ToListAsync();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Skema), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var skema = await _context.Skema.FindAsync(id);
            return skema == null ? NotFound() : Ok(skema);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(Skema skema)
        {
            await _context.Skema.AddAsync(skema);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = skema.AssignmentId }, skema);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, Skema skema)
        {
            if (id != skema.AssignmentId) return BadRequest();
            _context.Entry(skema).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var skemaToDelete = await _context.Skema.FindAsync(id);
            if (skemaToDelete == null) return NotFound();

            _context.Skema.Remove(skemaToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}
