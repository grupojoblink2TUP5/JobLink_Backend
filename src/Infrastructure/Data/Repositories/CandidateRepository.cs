using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly ApplicationContext _context;

        public CandidateRepository(ApplicationContext context)
        {
            _context = context;
        }

        public List<Candidate> GetAll()
        {
            return _context.Candidates
                .Include(c => c.User)
                .ToList();
        }

        public Candidate? GetById(int id)
        {
            return _context.Candidates
                .Include(c => c.User)
                .FirstOrDefault(c => c.Id == id);
        }

        public Candidate? GetByUserId(int userId)
        {
            return _context.Candidates
                .Include(c => c.User)
                .FirstOrDefault(c => c.UserId == userId);
        }

        public Candidate Create(Candidate candidate)
        {
            _context.Candidates.Add(candidate);
            return candidate;
        }

        public void Update(Candidate candidate)
        {
            _context.Candidates.Update(candidate);
        }

        public void Delete(Candidate candidate)
        {
            _context.Candidates.Remove(candidate);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}