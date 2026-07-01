using Domain.Enums;
namespace Application.DTOs.ApplicationHistory.Request;

public class UpdateApplicationHistoryRequest
{
    public string? Description { get; set; }
    public bool VisibleToCandidate { get; set; } = true;
}