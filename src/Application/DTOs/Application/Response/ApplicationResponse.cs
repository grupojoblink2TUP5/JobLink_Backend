namespace Application.DTOs.Application.Response;

public class ApplicationResponse
{
    public int Id { get; init; }
    public string CurrentStatus { get; init; }
    public DateTime AppliedAt { get; init; }
    public int UserId { get; init; }
    public int OfferId { get; init; }

    public ApplicationResponse(
        int id,
        string currentStatus,
        DateTime appliedAt,
        int userId,
        int offerId
    )
    {
        Id = id;
        CurrentStatus = currentStatus;
        AppliedAt = appliedAt;
        UserId = userId;
        OfferId = offerId;
    }
}