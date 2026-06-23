namespace Application.DTOs.ApplicationHistory.Request;

public class UpdateApplicationRequest
{
    public string Status { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool VisibleToCandidate { get; set; } = true;
}