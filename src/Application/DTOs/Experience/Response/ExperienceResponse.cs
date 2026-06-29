namespace Application.DTOs.Experience.Response;

public class ExperienceResponse
{
    public int Id { get; init; }
    public string? CompanyName { get; init; }
    public string? Position { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public string? Description { get; init; }
    public int UserId { get; init; }

    public ExperienceResponse(
        int id,
        string? companyName,
        string? position,
        DateTime startDate,
        DateTime? endDate,
        string? description,
        int userId
    )
    {
        Id = id;
        CompanyName = companyName;
        Position = position;
        StartDate = startDate;
        EndDate = endDate;
        Description = description;
        UserId = userId;
    }
}