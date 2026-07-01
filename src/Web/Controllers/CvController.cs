using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

[ApiController]
[Route("api/cv")]
public class CvController : ControllerBase
{
    private readonly ICvService _cvService;

    public CvController(ICvService cvService)
    {
        _cvService = cvService;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> UploadCv(IFormFile file)
    {
        var userIdClaim = User.FindFirstValue("sub")
            ?? User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!int.TryParse(userIdClaim, out var userId))
            return Unauthorized();

        if (file is null)
            return BadRequest(new { message = "El archivo es requerido." });

        var allowedExtensions = new[] { ".pdf", ".doc", ".docx" };
        var extension = Path.GetExtension(file.FileName).ToLower();

        if (!allowedExtensions.Contains(extension))
            return BadRequest(new { message = "Solo se permiten archivos PDF o Word." });

        using var stream = file.OpenReadStream();

        var result = await _cvService.UploadCvAsync(userId, stream, file.FileName);

        return Ok(result);
    }

    [HttpGet("{userId:int}")]
    public async Task<IActionResult> GetCv([FromRoute] int userId)
    {
        var cv = await _cvService.GetByUserIdAsync(userId);

        if (cv is null)
            return NotFound(new { message = $"CV not found for user. Id = {userId}" });

        return Ok(cv);
    }

    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> DeleteCv()
    {
        var userIdClaim = User.FindFirstValue("sub")
            ?? User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!int.TryParse(userIdClaim, out var userId))
            return Unauthorized();

        await _cvService.DeleteCvAsync(userId);
        return NoContent();
    }
}