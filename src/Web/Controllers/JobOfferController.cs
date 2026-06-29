using Application.DTOs.JobOffer.Request;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/job-offers")]
public class JobOfferController : ControllerBase
{
    private readonly IJobOfferService _jobOfferService;

    public JobOfferController(
        IJobOfferService jobOfferService)
    {
        _jobOfferService = jobOfferService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateJobOfferRequestDto request)
    {
        var jobOffer =
            await _jobOfferService.CreateAsync(request);

        return CreatedAtAction(
            nameof(GetById),
            new { id = jobOffer.Id },
            jobOffer);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var offers =
            await _jobOfferService.GetAllAsync();

        return Ok(offers);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var offer =
            await _jobOfferService.GetByIdAsync(id);

        if (offer is null)
            return NotFound();

        return Ok(offer);
    }

    [HttpGet("company/{companyId:int}")]
    public async Task<IActionResult> GetByCompany(
        int companyId)
    {
        var offers =
            await _jobOfferService
                .GetByCompanyIdAsync(companyId);

        return Ok(offers);
    }

    [HttpGet("open")]
    public async Task<IActionResult> GetOpenOffers()
    {
        var offers =
            await _jobOfferService
                .GetOpenOffersAsync();

        return Ok(offers);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateJobOfferRequestDto request)
    {
        await _jobOfferService.UpdateAsync(
            id,
            request);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _jobOfferService.DeleteAsync(id);

        return NoContent();
    }

    [HttpPatch("{id:int}/close")]
    public async Task<IActionResult> Close(int id)
    {
        await _jobOfferService.CloseAsync(id);

        return NoContent();
    }

    [HttpPatch("{id:int}/pause")]
    public async Task<IActionResult> Pause(int id)
    {
        await _jobOfferService.PauseAsync(id);

        return NoContent();
    }

    [HttpPatch("{id:int}/reopen")]
    public async Task<IActionResult> Reopen(int id)
    {
        await _jobOfferService.ReopenAsync(id);

        return NoContent();
    }
}