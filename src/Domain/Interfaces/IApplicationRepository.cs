using Domain.Entities;

namespace Domain.Interfaces;

public interface IApplicationRepository
{
    Task<List<Application>> GetAllAsync();

    Task<Application?> GetByIdAsync(int id);

    Task<List<Application>> GetByUserIdAsync(int userId);

    Task<List<Application>> GetByOfferIdAsync(int offerId);

    Task<Application?> GetByUserIdAndOfferIdAsync(int userId, int offerId);

    Application Create(Application application);

    void Update(Application application);

    void Delete(Application application);

    Task SaveChangesAsync();
}