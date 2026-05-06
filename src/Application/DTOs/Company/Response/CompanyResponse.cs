namespace Application.DTOs.Company.Response;

public class CompanyResponse
{
    public int Id { get; init; }

    public string? BusinessName { get; init; }

    public string? Cuit { get; init; }

    public string? Industry { get; init; }

    public string? Description { get; init; }

    public string? Website { get; init; }

    public string? Location { get; init; }

    public bool Approved { get; init; }

    public CompanyResponse(
        int id,
        string? businessName,
        string? cuit,
        string? industry,
        string? description,
        string? website,
        string? location,
        bool approved
    )
    {
        Id = id;
        BusinessName = businessName;
        Cuit = cuit;
        Industry = industry;
        Description = description;
        Website = website;
        Location = location;
        Approved = approved;
    }
}