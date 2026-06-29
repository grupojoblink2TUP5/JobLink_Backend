using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Application.Interfaces;
using Application.DTOs.Application.Request;
using Domain.Exceptions;
using System.Security.Claims;

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

        [HttpGet]
        public IActionResult GetAll()
        {
            var applications = _applicationService.GetAllApplications();
            return Ok(applications);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            try
            {
                var application = _applicationService.GetApplicationById(id);
                return Ok(application);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("user/{userId:int}")]
        public IActionResult GetByUserId([FromRoute] int userId)
        {
            var applications = _applicationService.GetApplicationsByUserId(userId);
            return Ok(applications);
        }

        [HttpGet("offer/{offerId:int}")]
        public IActionResult GetByOfferId([FromRoute] int offerId)
        {
            var applications = _applicationService.GetApplicationsByOfferId(offerId);
            return Ok(applications);
        }

        [HttpGet("user/{userId:int}/offer/{offerId:int}")]
        public IActionResult GetByUserIdAndOfferId([FromRoute] int userId, [FromRoute] int offerId)
        {
            var application = _applicationService.GetApplicationByUserIdAndOfferId(userId, offerId);
            if (application == null)
                return NotFound();

            return Ok(application);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create([FromBody] CreateApplicationRequest request)
        {
            var userIdClaim = User.FindFirstValue("sub")
                ?? User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdClaim, out var userId))
                return Unauthorized();

            try
            {
                var result = _applicationService.CreateApplication(userId, request);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (DuplicateApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateApplicationRequest request)
        {
            try
            {
                var result = _applicationService.UpdateApplication(id, request);
                return Ok(result);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var deleted = _applicationService.DeleteApplication(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}