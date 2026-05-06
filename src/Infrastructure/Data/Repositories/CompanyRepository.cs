using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationContext _context;

        public CompanyRepository(ApplicationContext context)
        {
            _context = context;
        }

        public List<Company> GetAll()
        {
            return _context.Companies.ToList();
        }

        public Company? GetById(int id)
        {
            return _context.Companies.FirstOrDefault(c => c.Id == id);
        }

        public Company Create(Company company)
        {
            _context.Companies.Add(company);

            return company;
        }

        public void Update(Company company)
        {
            _context.Companies.Update(company);
        }

        public void Delete(Company company)
        {
            _context.Companies.Remove(company);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}