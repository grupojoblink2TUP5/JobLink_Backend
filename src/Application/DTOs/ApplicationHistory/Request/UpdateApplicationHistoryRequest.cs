using Domain.Enums;
namespace Application.DTOs.ApplicationHistory.Request;

public class UpdateApplicationHistoryRequest
{
    public ApplicationStatus Status { get; set; } = ApplicationStatus.Applied;
    public string? Description { get; set; }
    public bool VisibleToCandidate { get; set; } = true;
}