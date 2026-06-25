namespace Domain.Entities;

public class Company
{
    public int Id { get; private set; }

    public string BusinessName { get; private set; }

    public string? ImageUrl { get; private set; }

    public string? ImagePublicId { get; private set; }

    public string Cuit { get; private set; }

    public string Sector { get; private set; }

    public string? Description { get; private set; }

    public string? Website { get; private set; }

    public bool Status { get; private set; }

    public int CreatedByRecruiterId { get; private set; }

    public int? ApprovedByAdminId { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime? ApprovedAt { get; private set; }


    public Company(
        string businessName,
        string cuit,
        string sector,
        string? description,
        string? website,
        int createdByRecruiterId)
    {
        BusinessName = businessName;
        Cuit = cuit;
        Sector = sector;
        Description = description;
        Website = website;
        CreatedByRecruiterId = createdByRecruiterId;

        CreatedAt = DateTime.UtcNow;
        Status = false;
    }

    public void SetLogo(
        string imageUrl,
        string imagePublicId)
    {
        ImageUrl = imageUrl;
        ImagePublicId = imagePublicId;
    }

    public void Approve(int adminId)
    {
        Status = true;
        ApprovedByAdminId = adminId;
        ApprovedAt = DateTime.UtcNow;
    }

    public void Update(
        string businessName,
        string sector,
        string? description,
        string? website)
    {
        BusinessName = businessName;
        Sector = sector;
        Description = description;
        Website = website;
    }
}