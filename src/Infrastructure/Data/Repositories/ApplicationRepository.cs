using Domain.Entities;
using ApplicationEntity = Domain.Entities.Application;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly ApplicationContext _context;

        public ApplicationRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<ApplicationEntity>> GetAllAsync()
        {
            return await _context.Applications.ToListAsync();
        }

        public async Task<ApplicationEntity?> GetByIdAsync(int id)
        {
            return await _context.Applications.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<ApplicationEntity>> GetByUserIdAsync(int userId)
        {
            return await _context.Applications.Where(a => a.UserId == userId).ToListAsync();
        }

        public async Task<ApplicationEntity?> GetByUserIdAndOfferIdAsync(int userId, int offerId)
        {
            return await _context.Applications.FirstOrDefaultAsync(a => a.UserId == userId && a.OfferId == offerId);
        }

        public async Task<List<ApplicationEntity>> GetByOfferIdAsync(int offerId)
        {
            return await _context.Applications.Where(a => a.OfferId == offerId).ToListAsync();
        }

        public ApplicationEntity Create(ApplicationEntity application)
        {
            _context.Applications.Add(application);
            return application;
        }

        public void Update(ApplicationEntity application)
        {
            _context.Applications.Update(application);
        }

        public void Delete(ApplicationEntity application)
        {
            _context.Applications.Remove(application);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}