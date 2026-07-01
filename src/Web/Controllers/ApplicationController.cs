using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Application.Interfaces;
using Application.DTOs.Application.Request;
using System.Security.Claims;
using Domain.Exceptions;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [Authorize(Roles = "Recruiter,Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var applications = await _applicationService.GetAllApplicationsAsync();
            return Ok(applications);
        }

        [Authorize(Roles = "Recruiter,Admin,Candidate")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var application = await _applicationService.GetApplicationByIdAsync(id);
            return Ok(application);
        }

        [Authorize(Roles = "Recruiter,Admin,Candidate")]
        [HttpGet("user/{userId:int}")]
        public async Task<IActionResult> GetByUserId([FromRoute] int userId)
        {
            try
            {
                var applications = await _applicationService.GetApplicationsByUserIdAsync(userId);
                return Ok(applications);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "Recruiter,Admin,Candidate")]
        [HttpGet("offer/{offerId:int}")]
        public async Task<IActionResult> GetByOfferId([FromRoute] int offerId)
        {
            var applications = await _applicationService.GetApplicationsByOfferIdAsync(offerId);
            return Ok(applications);
        }

        [Authorize(Roles = "Recruiter,Admin,Candidate")]
        [HttpGet("user/{userId:int}/offer/{offerId:int}")]
        public async Task<IActionResult> GetByUserIdAndOfferId([FromRoute] int userId, [FromRoute] int offerId)
        {
            var application = await _applicationService.GetApplicationByUserIdAndOfferIdAsync(userId, offerId);

            if (application is null)
                return NotFound();

            return Ok(application);
        }

        [Authorize(Roles = "Recruiter,Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateApplicationRequest request)
        {
            var userIdClaim = User.FindFirstValue("sub")
                ?? User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdClaim, out var userId))
                return Unauthorized();

            var result = await _applicationService.CreateApplicationAsync(userId, request);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [Authorize(Roles = "Recruiter,Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _applicationService.DeleteApplicationAsync(id);
            return NoContent();
        }
    }
}