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
        public IActionResult GetExperienceById([FromRoute] int id)
        {
            var experience = _experienceService.GetExperienceById(id);

            if (experience == null)
                return NotFound();

            return Ok(experience);
        }

        [HttpGet("candidate/{candidateId:int}")]
        public IActionResult GetExperienceByCandidateId([FromRoute] int candidateId)
        {
            var experience = _experienceService.GetExperienceByCandidateId(candidateId);

            if (experience == null)
                return NotFound();

            return Ok(experience);
        }

        [HttpPost]
        public IActionResult Create(CreateExperienceRequest request)
        {
            var result = _experienceService.CreateExperience(request);
            return CreatedAtAction(nameof(GetExperienceById), new { id = result.Id }, result);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateExperienceRequest request)
        {
            var result = _experienceService.UpdateExperience(id, request);
            return Ok(result);
        }
        
        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var deleted = _experienceService.DeleteExperience(id);

            if (!deleted)
            {
                return NotFound($"Experience with id {id} not found.");
            }

            return NoContent();
        }
    }
}