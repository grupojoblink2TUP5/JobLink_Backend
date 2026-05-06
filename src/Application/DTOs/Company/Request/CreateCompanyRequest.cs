namespace Application.DTOs.Company.Request;

public class CreateCompanyRequest
{
    public string? BusinessName { get; set; }

    public string? Cuit { get; set; }

    public string? Industry { get; set; }

    public string? Description { get; set; }

    public string? Website { get; set; }

    public string? Location { get; set; }
}