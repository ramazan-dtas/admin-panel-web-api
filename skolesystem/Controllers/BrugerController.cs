using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using skolesystem.DTOs;
using skolesystem.Models;
using skolesystem.Service;

namespace skolesystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrugerController : ControllerBase
    {
        private readonly IBrugerService _brugerService;

        public BrugerController(IBrugerService brugerService)
        {
            _brugerService = brugerService;
        }

        [HttpGet]
        public async Task<IEnumerable<BrugerReadDto>> GetBrugers()
        {
            var brugers = await _brugerService.GetAllBrugers();
            var brugerDtos = new List<BrugerReadDto>();
            foreach (var bruger in brugers)
            {
                brugerDtos.Add(new BrugerReadDto
                {
                    user_information_id = bruger.user_information_id,
                    name = bruger.name,
                    last_name = bruger.last_name,
                    phone = bruger.phone,
                    date_of_birth = bruger.date_of_birth,
                    address = bruger.address,
                    is_deleted = bruger.is_deleted,
                    gender_id = bruger.gender_id,
                    city_id = bruger.city_id,
                    user_id = bruger.user_id
                    
                });
            }
            return brugerDtos;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BrugerReadDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBrugerById(int id)
        {
            var bruger = await _brugerService.GetBrugerById(id);

            if (bruger == null)
            {
                return NotFound();
            }

            var brugerDto = new BrugerReadDto
            {
                user_information_id = bruger.user_information_id,
                name = bruger.name,
                last_name = bruger.last_name,
                phone = bruger.phone,
                date_of_birth = bruger.date_of_birth,
                address = bruger.address,
                is_deleted = bruger.is_deleted,
                gender_id = bruger.gender_id,
                city_id = bruger.city_id,
                user_id = bruger.user_id
                
            };

            return Ok(brugerDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateBruger(BrugerCreateDto brugerDto)
        {
            try
            {
                var bruger = new Bruger
                {
                    name = brugerDto.name,
                    last_name = brugerDto.last_name,
                    phone = brugerDto.phone,
                    date_of_birth = brugerDto.date_of_birth,
                    address = brugerDto.address,
                    is_deleted = brugerDto.is_deleted,
                    gender_id = brugerDto.gender_id,
                    city_id = brugerDto.city_id,
                    user_id = brugerDto.user_id
                };

                await _brugerService.AddBruger(bruger);

                return CreatedAtAction(nameof(GetBrugerById), new { id = bruger.user_information_id }, brugerDto);
            }
            catch (ArgumentException ex)
            {
                // User with the specified ID already exists
                return Conflict(ex.Message);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBruger(int id, BrugerUpdateDto brugerDto)
        {
            var existingBruger = await _brugerService.GetBrugerById(id);

            if (existingBruger == null)
            {
                return NotFound();
            }

            existingBruger.name = brugerDto.name;
            existingBruger.last_name = brugerDto.last_name;
            // Map other fields as needed

            await _brugerService.UpdateBruger(existingBruger);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBruger(int id)
        {
            var brugerToDelete = await _brugerService.GetBrugerById(id);

            if (brugerToDelete == null)
            {
                return NotFound();
            }

            await _brugerService.SoftDeleteBruger(id);

            return NoContent();
        }




    }
}
