using Domain.Enums;
namespace Application.DTOs.ApplicationHistory.Response;

public class ApplicationHistoryResponse
{
    public int Id { get; init; }
    public int ApplicationId { get; init; }
    public int ChangedByRecruiterId { get; init; }
    public ApplicationStatus Status { get; init; } = ApplicationStatus.Applied;
    public string? Description { get; init; }
    public DateTime ChangedAt { get; init; }
    public bool VisibleToCandidate { get; init; }

    public ApplicationHistoryResponse(
        int id,
        int applicationId,
        int changedByRecruiterId,
        ApplicationStatus status,
        string? description,
        DateTime changedAt,
        bool visibleToCandidate
    )
    {
        Id = id;
        ApplicationId = applicationId;
        ChangedByRecruiterId = changedByRecruiterId;
        Status = status;
        Description = description;
        ChangedAt = changedAt;
        VisibleToCandidate = visibleToCandidate;
    }
}