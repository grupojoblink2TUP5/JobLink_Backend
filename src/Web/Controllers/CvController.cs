using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.DTOs.Cv.Request;
using Microsoft.AspNetCore.Authorization;
using Domain.Exceptions;

[ApiController]
[Route("api/cv")]
public class CvController : ControllerBase
{
    private readonly ICvService _cvService;

    public CvController(
        ICvService cvService)
    {
        _cvService = cvService;
    }

    [HttpPost]
    public async Task<IActionResult> UploadCv(
        [FromForm] int userId,
        IFormFile file)
    {
        if (file == null)
            return BadRequest();

        var allowedExtensions =
            new[] { ".pdf", ".doc", ".docx" };

        var extension =
            Path.GetExtension(file.FileName)
                .ToLower();

        if (!allowedExtensions.Contains(extension))
        {
            return BadRequest(
                "Solo PDF o Word");
        }

        using var stream =
            file.OpenReadStream();

        var result =
            await _cvService
                .UploadCvAsync(
                    userId,
                    stream,
                    file.FileName);

        return Ok(result);
    }

    [HttpGet("{userId:int}")]
    public async Task<IActionResult> GetCv(
        int userId)
    {
        var cv =
            await _cvService
                .GetByUserIdAsync(userId);

        if (cv is null)
            return NotFound();

        return Ok(cv);
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteCv(
        int userId)
    {
        await _cvService
            .DeleteCvAsync(userId);

        return NoContent();
    }
}