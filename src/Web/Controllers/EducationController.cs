using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.DTOs.Education.Request;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EducationController : ControllerBase
    {
        private readonly IEducationService _educationService;

        public EducationController(IEducationService educationService)
        {
            _educationService = educationService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var educations = _educationService.GetAllEducations();

            return Ok(educations);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var education = _educationService.GetEducationById(id);

            if (education == null)
            {
                return NotFound();
            }

            return Ok(education);
        }

        [HttpGet("user/{userId:int}")]
        public IActionResult GetByUserId([FromRoute] int userId)
        {
            var educations = _educationService.GetEducationsByUserId(userId);

            return Ok(educations);
        }

        [Authorize(Roles = "Candidate")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEducationRequest request)
        {
            var result = await _educationService.CreateEducationAsync(request);

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id },
                result
            );
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(
            [FromRoute] int id,
            [FromBody] UpdateEducationRequest request
        )
        {
            _educationService.UpdateEducation(id, request);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _educationService.DeleteEducation(id);

            return NoContent();
        }
    }
}