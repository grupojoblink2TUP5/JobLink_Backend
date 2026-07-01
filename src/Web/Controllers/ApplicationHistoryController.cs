using Application.DTOs.ApplicationHistory.Request;
using Application.Interfaces;
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

        [Authorize(Roles = "Recruiter,Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var applicationHistories = await _applicationHistoryService.GetAllApplicationHistoriesAsync();
            return Ok(applicationHistories);
        }

        [Authorize(Roles = "Recruiter,Admin,Candidate")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var applicationHistory = await _applicationHistoryService.GetApplicationHistoryByIdAsync(id);
            return Ok(applicationHistory);
        }

        [HttpGet("application/{applicationId:int}")]
        public async Task<IActionResult> GetByApplicationId([FromRoute] int applicationId)
        {
            var applicationHistories = await _applicationHistoryService.GetApplicationHistoriesByApplicationIdAsync(applicationId);
            return Ok(applicationHistories);
        }

        [Authorize(Roles = "Recruiter,Admin")]
        [HttpPost("application/{applicationId:int}")]
        public async Task<IActionResult> Create(
            [FromRoute] int applicationId,
            [FromBody] CreateApplicationHistoryRequest request
        )
        {
            var result = await _applicationHistoryService.CreateApplicationHistoryAsync(
                applicationId,
                request
            );

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id },
                result
            );
        }

        [Authorize(Roles = "Recruiter,Admin")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            [FromRoute] int id,
            [FromBody] UpdateApplicationHistoryRequest request
        )
        {
            var result = await _applicationHistoryService.UpdateApplicationHistoryAsync(id, request);
            return Ok(result);
        }

        [Authorize(Roles = "Recruiter,Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _applicationHistoryService.DeleteApplicationHistoryAsync(id);
            return NoContent();
        }
    }
}