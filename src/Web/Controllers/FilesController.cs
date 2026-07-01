using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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

    [Authorize(Roles = "Recruiter,Admin,Candidate")]
    [HttpPost("upload-image")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Archivo inválido");

        using var stream = file.OpenReadStream();

        var uploadResult =
            await _cloudinaryService
        .UploadImageAsync(
            stream,
            file.FileName);

        return Ok(uploadResult);


    }

    [Authorize(Roles = "Recruiter,Admin,Candidate")]
    [HttpPost("upload-document")]
    public async Task<IActionResult> UploadDocument(
    IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Archivo inválido");

        var allowedExtensions =
            new[] { ".pdf", ".doc", ".docx" };

        var extension =
            Path.GetExtension(file.FileName)
                .ToLower();

        if (!allowedExtensions.Contains(extension))
        {
            return BadRequest(
                "Solo se permiten archivos PDF o Word");
        }

        using var stream = file.OpenReadStream();

        var uploadResult =
            await _cloudinaryService
        .UploadDocumentAsync(
            stream,
            file.FileName);

        return Ok(uploadResult);
    }
}
