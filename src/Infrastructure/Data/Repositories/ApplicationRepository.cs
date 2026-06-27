using Domain.Entities;
using ApplicationEntity = Domain.Entities.Application;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly ApplicationContext _context;

        public ApplicationRepository(ApplicationContext context)
        {
            _context = context;
        }

        public List<ApplicationEntity> GetAll()
        {
            return _context.Applications.ToList();
        }

        public ApplicationEntity? GetById(int id)
        {
            return _context.Applications.FirstOrDefault(a => a.Id == id);
        }

        public List<ApplicationEntity> GetByUserId(int userId)
        {
            return _context.Applications.Where(a => a.UserId == userId).ToList();
        }

        public ApplicationEntity? GetByUserIdAndOfferId(int userId, int offerId)
        {
            return _context.Applications.FirstOrDefault(a => a.UserId == userId && a.OfferId == offerId);
        }

        public List<ApplicationEntity> GetByOfferId(int offerId)
        {
            return _context.Applications.Where(a => a.OfferId == offerId).ToList();
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

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}