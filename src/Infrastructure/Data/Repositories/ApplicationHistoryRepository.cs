using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ApplicationHistoryRepository : IApplicationHistoryRepository
    {
        private readonly ApplicationContext _context;

        public ApplicationHistoryRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<ApplicationHistory>> GetAllAsync()
        {
            return await _context.ApplicationHistories.ToListAsync();
        }

        public async Task<ApplicationHistory?> GetByIdAsync(int id)
        {
            return await _context.ApplicationHistories.FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<List<ApplicationHistory>> GetByApplicationIdAsync(int applicationId)
        {
            return await _context.ApplicationHistories
                .Where(n => n.ApplicationId == applicationId)
                .ToListAsync();
        }

        public ApplicationHistory Create(ApplicationHistory applicationHistory)
        {
            _context.ApplicationHistories.Add(applicationHistory);
            return applicationHistory;
        }

        public void Update(ApplicationHistory applicationHistory)
        {
            _context.ApplicationHistories.Update(applicationHistory);
        }

        public void Delete(ApplicationHistory applicationHistory)
        {
            _context.ApplicationHistories.Remove(applicationHistory);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}