using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class JobOfferRepository : IJobOfferRepository
{
    private readonly ApplicationContext _context;

    public JobOfferRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<JobOffer?> GetByIdAsync(int id)
    {
        return await _context.JobOffers
            .FirstOrDefaultAsync(j => j.Id == id);
    }

    public async Task<List<JobOffer>> GetAllAsync()
    {
        return await _context.JobOffers
            .ToListAsync();
    }

    public async Task<List<JobOffer>> GetByCompanyIdAsync(int companyId)
    {
        return await _context.JobOffers
            .Where(j => j.CompanyId == companyId)
            .ToListAsync();
    }

    public async Task<List<JobOffer>> GetOpenOffersAsync()
    {
        return await _context.JobOffers
            .Where(j => j.Status == JobOfferStatus.Open)
            .ToListAsync();
    }

    public async Task AddAsync(JobOffer jobOffer)
    {
        await _context.JobOffers.AddAsync(jobOffer);

        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(JobOffer jobOffer)
    {
        _context.JobOffers.Update(jobOffer);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(JobOffer jobOffer)
    {
        _context.JobOffers.Remove(jobOffer);

        await _context.SaveChangesAsync();
    }
}