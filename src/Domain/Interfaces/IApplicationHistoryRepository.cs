using Domain.Entities;

namespace Domain.Interfaces;

public interface IApplicationHistoryRepository
{
    List<ApplicationHistory> GetAll();

    ApplicationHistory? GetById(int id);

    List<ApplicationHistory> GetByApplicationId(int applicationId);

    ApplicationHistory Create(ApplicationHistory applicationHistory);

    void Update(ApplicationHistory applicationHistory);

    void Delete(ApplicationHistory applicationHistory);

    void SaveChanges();
}