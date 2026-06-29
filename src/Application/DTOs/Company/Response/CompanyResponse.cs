namespace Application.DTOs.Company.Response;

public class CompanyResponseDto
{
    public int Id { get; set; }

    public string BusinessName { get; set; } = string.Empty;

    public string? ImageUrl { get; set; }

    public string Cuit { get; set; } = string.Empty;

    public string Sector { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string? Website { get; set; }

    public bool Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedByRecruiterId { get; set; }

    public DateTime? ApprovedAt { get; set; }

    public int? ApprovedByAdminId { get; set; }
}