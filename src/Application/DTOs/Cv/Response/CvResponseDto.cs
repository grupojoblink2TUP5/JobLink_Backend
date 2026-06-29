namespace Application.DTOs.Cv.Response;

public class CvResponseDto
{
    public int Id { get; set; }

    public string Url { get; set; } = string.Empty;

    public string PublicId { get; set; } = string.Empty;

    public int UserId { get; set; }
}