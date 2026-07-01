using Domain.Enums;
namespace Application.DTOs.ApplicationHistory.Request;

public class CreateApplicationHistoryRequest
{
    public int ChangedByRecruiterId { get; set; }
    public ApplicationStatus Status { get; set; } = ApplicationStatus.Applied;
    public string? Description { get; set; }
    public bool VisibleToCandidate { get; set; } = true;
}