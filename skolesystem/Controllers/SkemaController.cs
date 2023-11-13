using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using skolesystem.Data;
using skolesystem.DTOs;
using skolesystem.Models;
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
        private readonly ISkemaRepository _skemaRepository;

        public SkemaController(ISkemaRepository skemaRepository)
        {
            _skemaRepository = skemaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var skemaList = await _skemaRepository.GetAll();
            return Ok(skemaList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var skema = await _skemaRepository.GetById(id);

            if (skema == null)
            {
                return NotFound();
            }

            return Ok(skema);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SkemaCreateDto skemaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var skemaId = await _skemaRepository.Create(new Skema
            {
                user_subject_id = skemaDto.user_subject_id,
                day_of_week = skemaDto.day_of_week,
                start_time = skemaDto.start_time,
                end_time = skemaDto.end_time,
                class_id = skemaDto.class_id
            });

            return CreatedAtAction(nameof(GetById), new { id = skemaId }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SkemaCreateDto skemaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _skemaRepository.Update(id, skemaDto);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _skemaRepository.Delete(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }

}
