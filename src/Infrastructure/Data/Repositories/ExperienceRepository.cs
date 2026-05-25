using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class ExperienceRepository : IExperienceRepository
{
    private readonly ApplicationContext _context;

    public ExperienceRepository(ApplicationContext context)
    {
        _context = context;
    }

    public List<Experience> GetAll()
    {
        return _context.Experiences.ToList();
    }

    public Experience? GetById(int id)
    {
        return _context.Experiences.FirstOrDefault(e => e.Id == id);
    }

    public Experience? GetByCandidateId(int candidateId)
    {
        return _context.Experiences.FirstOrDefault(e => e.CandidateId == candidateId);
    }

    public Experience Create(Experience experience)
    {
        _context.Experiences.Add(experience);
        _context.SaveChanges();
        return experience;
    }

    public void Update(Experience experience)
    {
        _context.Experiences.Update(experience);
        _context.SaveChanges();
    }

    public void Delete(Experience experience)
    {
        _context.Experiences.Remove(experience);
        _context.SaveChanges();
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}
