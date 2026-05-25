using Domain.Entities;

namespace Domain.Interfaces;

public interface IExperienceRepository
{
    List<Experience> GetAll();

    Experience? GetById(int id);

    Experience? GetByCandidateId(int candidateId);

    Experience Create(Experience experience);

    void Update(Experience experience);

    void Delete(Experience experience);

    void SaveChanges();
}