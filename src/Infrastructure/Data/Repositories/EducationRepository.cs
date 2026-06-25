using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class EducationRepository : IEducationRepository
    {
        private readonly ApplicationContext _context;

        public EducationRepository(ApplicationContext context)
        {
            _context = context;
        }

        public List<Education> GetAll()
        {
            return _context.Educations.ToList();
        }

        public Education? GetById(int id)
        {
            return _context.Educations.FirstOrDefault(e => e.Id == id);
        }

        public List<Education> GetByUserId(int userId)
        {
            return _context.Educations
                .Where(e => e.UserId == userId)
                .ToList();
        }

        public Education Create(Education education)
        {
            _context.Educations.Add(education);

            return education;
        }

        public void Update(Education education)
        {
            _context.Educations.Update(education);
        }

        public void Delete(Education education)
        {
            _context.Educations.Remove(education);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}