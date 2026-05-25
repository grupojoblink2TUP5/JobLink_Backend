using Domain.Entities;

namespace Application.DTOs.Experience.Request;

public class CreateExperienceRequest
{
    public string? CompanyName { get; set; }
    public string? Position { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Description { get; set; }
    public int CandidateId { get; set; }
}