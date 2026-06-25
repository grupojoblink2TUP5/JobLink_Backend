using Domain.Enums;
namespace Application.DTOs.ApplicationHistory.Request;

public class CreateApplicationRequest
{
    public ApplicationHistoryStatus Status { get; set; } = ApplicationHistoryStatus.Applied;
    public string? Description { get; set; }
    public bool VisibleToCandidate { get; set; } = true;
}