using Domain.Entities;

namespace Domain.Interfaces;

public interface IExperienceRepository
{
    List<Experience> GetAll();

    Experience? GetById(int id);

    List<Experience> GetByUserId(int userId);

    Experience Create(Experience experience);

    void Update(Experience experience);

    void Delete(Experience experience);

    void SaveChanges();
}