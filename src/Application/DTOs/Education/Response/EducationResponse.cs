namespace Application.DTOs.Education.Response;

public class EducationResponse
{
    public int Id { get; init; }
    public string? InstitutionName { get; init; }
    public string? Degree { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public int UserId { get; init; }

    public EducationResponse(
        int id,
        string? institutionName,
        string? degree,
        DateTime startDate,
        DateTime? endDate,
        int userId
    )
    {
        Id = id;
        InstitutionName = institutionName;
        Degree = degree;
        StartDate = startDate;
        EndDate = endDate;
        UserId = userId;
    }
}