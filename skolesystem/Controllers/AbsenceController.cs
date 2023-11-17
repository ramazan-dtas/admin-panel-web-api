using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using skolesystem.DTOs;
using skolesystem.Models;
using skolesystem.Service;

namespace skolesystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AbsenceController : ControllerBase
    {
        private readonly IAbsenceService _absenceService;

        public AbsenceController(IAbsenceService absenceService)
        {
            _absenceService = absenceService;
        }

        [HttpGet]
        public async Task<IEnumerable<AbsenceReadDto>> GetAbsences()
        {
            var absences = await _absenceService.GetAllAbsences();
            var absenceDtos = new List<AbsenceReadDto>();
            foreach (var absence in absences)
            {
                absenceDtos.Add(new AbsenceReadDto
                {
                    absence_id = absence.absence_id,
                    user_id = absence.user_id,
                    teacher_id = absence.teacher_id,
                    class_id = absence.class_id,
                    absence_date = absence.absence_date,
                    reason = absence.reason,
                    is_deleted = absence.is_deleted
                });
            }
            return absenceDtos;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AbsenceReadDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAbsenceById(int id)
        {
            var absence = await _absenceService.GetAbsenceById(id);

            if (absence == null)
            {
                return NotFound();
            }

            var absenceDto = new AbsenceReadDto
            {
                absence_id = absence.absence_id,
                user_id = absence.user_id,
                teacher_id = absence.teacher_id,
                class_id = absence.class_id,
                absence_date = absence.absence_date,
                reason = absence.reason,
                is_deleted = absence.is_deleted
            };

            return Ok(absenceDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateAbsence(AbsenceCreateDto absenceDto)
        {
            var absence = new Absence
            {
                user_id = absenceDto.user_id,
                teacher_id = absenceDto.teacher_id,
                class_id = absenceDto.class_id,
                absence_date = absenceDto.absence_date,
                reason = absenceDto.reason,
                
            };

            await _absenceService.CreateAbsence(absence);

            return CreatedAtAction(nameof(GetAbsenceById), new { id = absence.absence_id }, absenceDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAbsence(int id, AbsenceUpdateDto absenceDto)
        {
            var existingAbsence = await _absenceService.GetAbsenceById(id);

            if (existingAbsence == null)
            {
                return NotFound();
            }

            // Pass absenceDto directly to the UpdateAbsence method
            await _absenceService.UpdateAbsence(id, absenceDto);

            return NoContent();
        }




        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAbsence(int id)
        {
            var absenceToDelete = await _absenceService.GetAbsenceById(id);

            if (absenceToDelete == null)
            {
                return NotFound();
            }

            await _absenceService.SoftDeleteAbsence(id);

            return NoContent();
        }
    }
}
