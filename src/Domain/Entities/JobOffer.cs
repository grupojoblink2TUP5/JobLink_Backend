using Domain.Enums;

namespace Domain.Entities;

public class JobOffer
{
    public int Id { get; private set; }

    public string JobTitle { get; private set; }

    public string Description { get; private set; }

    public decimal Salary { get; private set; }

    public string Location { get; private set; }

    public OfferType OfferType { get; private set; }

    public JobOfferStatus Status { get; private set; }

    public DateTime PublicationDate { get; private set; }

    public DateTime ClosingDate { get; private set; }

    public int CompanyId { get; private set; }

    public int CreatedByRecruiterId { get; private set; }


    public JobOffer(
    string jobTitle,
    string description,
    decimal salary,
    string location,
    OfferType offerType,
    DateTime closingDate,
    int companyId,
    int createdByRecruiterId)
    {
        JobTitle = jobTitle;
        Description = description;
        Salary = salary;
        Location = location;
        OfferType = offerType;

        CompanyId = companyId;
        CreatedByRecruiterId = createdByRecruiterId;

        PublicationDate = DateTime.UtcNow;
        ClosingDate = closingDate;

        Status = JobOfferStatus.Open;
    }

    public void Close()
    {
        Status = JobOfferStatus.Closed;
    }

    public void Pause()
    {
        Status = JobOfferStatus.Paused;
    }

    public void Reopen()
    {
        Status = JobOfferStatus.Open;
    }

    public void Update(
        string title,
        string description,
        decimal salary,
        string location,
        OfferType type,
        DateTime closingDate)
    {
        JobTitle = title;
        Description = description;
        Salary = salary;
        Location = location;
        OfferType = type;
        ClosingDate = closingDate;
    }
}