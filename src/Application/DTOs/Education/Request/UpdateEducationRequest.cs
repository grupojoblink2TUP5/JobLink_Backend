namespace Application.DTOs.Education.Request;

public class UpdateEducationRequest
{
    public string? InstitutionName { get; set; }
    public string? Degree { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}