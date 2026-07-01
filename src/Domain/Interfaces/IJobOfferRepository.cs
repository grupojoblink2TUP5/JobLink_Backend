using Domain.Entities;

namespace Domain.Interfaces;

public interface IJobOfferRepository
{
    Task<JobOffer?> GetByIdAsync(int id);

    Task<List<JobOffer>> GetAllAsync();

    Task<List<JobOffer>> GetByCompanyIdAsync(int companyId);

    Task<List<JobOffer>> GetOpenOffersAsync();

    Task AddAsync(JobOffer jobOffer);

    Task UpdateAsync(JobOffer jobOffer);

    Task DeleteAsync(JobOffer jobOffer);
}