using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.DTOs.Experience.Request;
using Microsoft.AspNetCore.Authorization;
using Domain.Exceptions;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExperienceController : ControllerBase
    {
        private readonly IExperienceService _experienceService;

        public ExperienceController(IExperienceService experienceService)
        {
            _experienceService = experienceService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var experiences = _experienceService.GetAllExperiences();

            return Ok(experiences);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var experience = _experienceService.GetExperienceById(id);

            if (experience == null)
            {
                return NotFound();
            }

            return Ok(experience);
        }

        [HttpGet("user/{userId:int}")]
        public IActionResult GetByUserId([FromRoute] int userId)
        {
            var experiences = _experienceService.GetExperiencesByUserId(userId);

            return Ok(experiences);
        }

        [Authorize(Roles = "Candidate")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateExperienceRequest request)
        {
            var result = await _experienceService.CreateExperienceAsync(request);

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id },
                result
            );
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(
            [FromRoute] int id,
            [FromBody] UpdateExperienceRequest request
        )
        {
            try
            {
                var result = _experienceService.UpdateExperience(id, request);

                return Ok(result);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var success = _experienceService.DeleteExperience(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}