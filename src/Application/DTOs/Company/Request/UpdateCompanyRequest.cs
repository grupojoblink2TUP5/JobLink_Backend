namespace Application.DTOs.Company.Request;

public class UpdateCompanyRequestDto
{
    public string BusinessName { get; set; } = string.Empty;

    public string Sector { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string? Website { get; set; }
}