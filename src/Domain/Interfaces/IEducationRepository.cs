using Domain.Entities;

namespace Domain.Interfaces;

public interface IEducationRepository
{
    List<Education> GetAll();

    Education? GetById(int id);

    List<Education> GetByUserId(int userId);

    Education Create(Education education);

    void Update(Education education);

    void Delete(Education education);

    void SaveChanges();
}