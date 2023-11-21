using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using skolesystem.Data;
using skolesystem.DTOs;
using skolesystem.DTOs.Enrollment.Request;
using skolesystem.DTOs.Enrollment.Response;
using skolesystem.DTOs.Skema.Request;
using skolesystem.DTOs.Skema.Response;
using skolesystem.Models;
using skolesystem.Service.EnrollmentService;
using skolesystem.Service.SkemaService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace skolesystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkemaController : ControllerBase
    {
        private readonly ISkemaService _SkemaService;

        public SkemaController(ISkemaService SkemaService)
        {
            _SkemaService = SkemaService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<SkemaReadDto> Enrollments = await _SkemaService.GetAllSkema();

                if (Enrollments == null)
                {
                    return Problem("Got no data, not even an empty list, this is unexpected");
                }

                if (Enrollments.Count == 0)
                {
                    return NoContent();
                }

                return Ok(Enrollments);

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("ByClass/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllEnrollmentsByClass([FromRoute] int id)
        {
            try
            {
                List<SkemaReadDto> Enrollments = await _SkemaService.GetAllSkemaByClass(id);

                if (Enrollments == null)
                {
                    return Problem("Got no data, not even an empty list, this is unexpected");
                }

                if (Enrollments.Count == 0)
                {
                    return NoContent();
                }

                return Ok(Enrollments);

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                SkemaReadDto Enrollments = await _SkemaService.GetById(id);

                if (Enrollments == null)
                {
                    return NotFound();
                }

                return Ok(Enrollments);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] SkemaCreateDto newEnrollment)
        {
            try
            {
                SkemaReadDto Enrollments = await _SkemaService.Create(newEnrollment);

                if (Enrollments == null)
                {
                    return Problem("Enrollment was not created, something went wrong");
                }

                return Ok(Enrollments);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] SkemaUpdateDto updateEnrollment)
        {
            try
            {
                SkemaReadDto Enrollments = await _SkemaService.Update(id, updateEnrollment);

                if (Enrollments == null)
                {
                    return Problem("Enrollment was not updated, something went wrong");
                }

                return Ok(Enrollments);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                bool result = await _SkemaService.Delete(id);

                if (!result)
                {
                    return Problem("Enrollment was not deleted, something went wrong");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }

}
