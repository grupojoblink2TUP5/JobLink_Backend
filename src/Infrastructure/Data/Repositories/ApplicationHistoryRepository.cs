using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class ApplicationHistoryRepository : IApplicationHistoryRepository
    {
        private readonly ApplicationContext _context;

        public ApplicationHistoryRepository(ApplicationContext context)
        {
            _context = context;
        }

        public List<ApplicationHistory> GetAll()
        {
            return _context.ApplicationHistories.ToList();
        }

        public ApplicationHistory? GetById(int id)
        {
            return _context.ApplicationHistories.FirstOrDefault(n => n.Id == id);
        }

        public List<ApplicationHistory> GetByApplicationId(int applicationId)
        {
            return _context.ApplicationHistories.Where(n => n.ApplicationId == applicationId).ToList();
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

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}