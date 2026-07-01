using Domain.Entities;

namespace Domain.Interfaces;

public interface IApplicationHistoryRepository
{
    Task<List<ApplicationHistory>> GetAllAsync();

    Task<ApplicationHistory?> GetByIdAsync(int id);

    Task<List<ApplicationHistory>> GetByApplicationIdAsync(int applicationId);

    ApplicationHistory Create(ApplicationHistory applicationHistory);

    void Update(ApplicationHistory applicationHistory);

    void Delete(ApplicationHistory applicationHistory);

    Task SaveChangesAsync();
}