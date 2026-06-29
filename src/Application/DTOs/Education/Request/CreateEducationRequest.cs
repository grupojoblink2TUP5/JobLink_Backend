namespace Application.DTOs.Education.Request;

public class CreateEducationRequest
{
    public string? InstitutionName { get; set; }
    public string? Degree { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int UserId { get; set; }
}