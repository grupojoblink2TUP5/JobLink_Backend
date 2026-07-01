using Domain.Enums;

namespace Application.DTOs.JobOffer.Response;

public class JobOfferResponseDto
{
    public int Id { get; set; }

    public string JobTitle { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Salary { get; set; }

    public OfferType OfferType { get; set; }

    public string Location { get; set; } = string.Empty;

    public DateTime PublicationDate { get; set; }

    public DateTime ClosingDate { get; set; }

    public JobOfferStatus Status { get; set; }

    public int CompanyId { get; set; }

    public int CreatedByRecruiterId { get; set; }
}