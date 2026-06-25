using System.Text.Json.Serialization;

namespace Application.DTOs.Cloudinary.Response;

public class CloudinaryUploadResultDto
{
    public string Url { get; set; } = string.Empty;

    public string PublicId { get; set; } = string.Empty;
}