using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.DTOs.Candidate.Request;
using Microsoft.AspNetCore.Authorization;
using Domain.Exceptions;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var candidates = _candidateService.GetAllCandidates();

            return Ok(candidates);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var candidate = _candidateService.GetCandidateById(id);

            if (candidate == null)
            {
                return NotFound();
            }

            return Ok(candidate);
        }

        [HttpGet("user/{userId:int}")]
        public IActionResult GetByUserId([FromRoute] int userId)
        {
                var candidates = _candidateService.GetCandidateByUserId(userId);

            return Ok(candidates);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create([FromBody] CreateCandidateRequest request)
        {
            var result = _candidateService.CreateCandidate(request);

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id },
                result
            );
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(
            [FromRoute] int id,
            [FromBody] UpdateCandidateRequest request
        )
        {
            try
            {
                var result = _candidateService.UpdateCandidate(id, request);

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
            var success = _candidateService.DeleteCandidate(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
