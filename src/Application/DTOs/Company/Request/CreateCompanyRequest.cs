namespace Application.DTOs.Company.Request;

public class CreateCompanyRequestDto
{
    public string BusinessName { get; set; } = string.Empty;

    public string Cuit { get; set; } = string.Empty;

    public string Sector { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string? Website { get; set; }

    public int CreatedByRecruiterId { get; set; }
}