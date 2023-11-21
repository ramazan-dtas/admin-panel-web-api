using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using skolesystem.Models;
using skolesystem.Service;
using skolesystem.DTOs.Absence.Response;
using skolesystem.DTOs.Absence.Request;
using skolesystem.DTOs.UserSubmission.Response;
using skolesystem.DTOs.UserSubmission.Request;
using skolesystem.Service.AbsenceService;
using skolesystem.Service.UserSubmissionService;

namespace skolesystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AbsenceController : ControllerBase
    {
        private readonly IAbsenceService _UserSubmissionService;

        public AbsenceController(IAbsenceService UserSubmissionService)
        {
            _UserSubmissionService = UserSubmissionService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<AbsenceReadDto> UserSubmissions = await _UserSubmissionService.GetAllAbsences();

                if (UserSubmissions == null)
                {
                    return Problem("Got no data, not even an empty list, this is unexpected");
                }

                if (UserSubmissions.Count == 0)
                {
                    return NoContent();
                }

                return Ok(UserSubmissions);

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("ByAbsenceClasse/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllEnrollmentsByAssignment([FromRoute] int id)
        {
            try
            {
                List<AbsenceReadDto> assignments = await _UserSubmissionService.GetAllAbsencebyClasse(id);

                if (assignments == null)
                {
                    return Problem("Got no data, not even an empty list, this is unexpected");
                }

                if (assignments.Count == 0)
                {
                    return NoContent();
                }

                return Ok(assignments);

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("ByAbsenceUser/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllEnrollmentsByUser([FromRoute] int id)
        {
            try
            {
                List<AbsenceReadDto> users = await _UserSubmissionService.GetAllAbsencebyUser(id);

                if (users == null)
                {
                    return Problem("Got no data, not even an empty list, this is unexpected");
                }

                if (users.Count == 0)
                {
                    return NoContent();
                }

                return Ok(users);

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
                AbsenceReadDto UserSubmissions = await _UserSubmissionService.GetAbsenceById(id);

                if (UserSubmissions == null)
                {
                    return NotFound();
                }

                return Ok(UserSubmissions);
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
        public async Task<IActionResult> Create([FromBody] AbsenceCreateDto newUserSubmission)
        {
            try
            {
                AbsenceReadDto UserSubmissions = await _UserSubmissionService.CreateAbsence(newUserSubmission);

                if (UserSubmissions == null)
                {
                    return Problem("UserSubmission was not created, something went wrong");
                }

                return Ok(UserSubmissions);
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
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] AbsenceUpdateDto updateUserSubmission)
        {
            try
            {
                AbsenceReadDto UserSubmissions = await _UserSubmissionService.UpdateAbsence(id, updateUserSubmission);

                if (UserSubmissions == null)
                {
                    return Problem("UserSubmission was not updated, something went wrong");
                }

                return Ok(UserSubmissions);
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
                bool result = await _UserSubmissionService.GetDeletedAbsences(id);

                if (!result)
                {
                    return Problem("UserSubmission was not deleted, something went wrong");
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
