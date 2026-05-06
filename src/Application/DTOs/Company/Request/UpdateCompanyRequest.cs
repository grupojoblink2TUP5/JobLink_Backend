namespace Application.DTOs.Company.Request;

public class UpdateCompanyRequest
{
    public string? Industry { get; set; }
    public string? Description { get; set; }

    public string? Website { get; set; }

    public string? Location { get; set; }
}