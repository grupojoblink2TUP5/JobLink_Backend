using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/files")]
public class FilesController : ControllerBase
{
    private readonly ICloudinaryService _cloudinaryService;

    public FilesController(
        ICloudinaryService cloudinaryService)
    {
        _cloudinaryService = cloudinaryService;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Archivo inválido");

        using var stream = file.OpenReadStream();

        var url = await _cloudinaryService
            .UploadImageAsync(stream, file.FileName);

        return Ok(new
        {
            ImageUrl = url
        });
    }
}