using Application.DTOs.ApplicationHistory.Request;
using Application.Interfaces;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationHistoryController : ControllerBase
    {
        private readonly IApplicationHistoryService _applicationHistoryService;

        public ApplicationHistoryController(IApplicationHistoryService applicationHistoryService)
        {
            _applicationHistoryService = applicationHistoryService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var applicationHistories = _applicationHistoryService.GetAllApplicationHistories();
            return Ok(applicationHistories);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var applicationHistory = _applicationHistoryService.GetApplicationHistoryById(id);

            if (applicationHistory == null)
            {
                return NotFound();
            }

            return Ok(applicationHistory);
        }

        [HttpGet("application/{applicationId:int}")]
        public IActionResult GetByApplicationId([FromRoute] int applicationId)
        {
            var applicationHistories = _applicationHistoryService.GetApplicationHistoriesByApplicationId(applicationId);
            return Ok(applicationHistories);
        }

        [Authorize(Roles = "Recruiter,Admin")]
        [HttpPost("application/{applicationId:int}")]
        public IActionResult Create(
            [FromRoute] int applicationId,
            [FromBody] CreateApplicationRequest request
        )
        {
            var result = _applicationHistoryService.CreateApplicationHistory(
                applicationId,
                request
            );

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id },
                result
            );
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(
            [FromRoute] int id,
            [FromBody] UpdateApplicationRequest request
        )
        {
            try
            {
                var result = _applicationHistoryService.UpdateApplicationHistory(id, request);
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
            var success = _applicationHistoryService.DeleteApplicationHistory(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}