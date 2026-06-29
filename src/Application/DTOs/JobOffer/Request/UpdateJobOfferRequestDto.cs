using Domain.Enums;

namespace Application.DTOs.JobOffer.Request;

public class UpdateJobOfferRequestDto
{
    public string JobTitle { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Salary { get; set; }

    public OfferType OfferType { get; set; }

    public string Location { get; set; } = string.Empty;

    public DateTime ClosingDate { get; set; }
}