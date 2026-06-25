using System.Text.Json.Serialization;

namespace Application.DTOs.Cloudinary.Response;

public class CloudinaryResponseDto
{
    [JsonPropertyName("secure_url")]
    public string SecureUrl { get; set; } = string.Empty;

    [JsonPropertyName("public_id")]
    public string PublicId { get; set; } = string.Empty;
}
