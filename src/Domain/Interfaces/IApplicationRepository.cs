using Domain.Entities;

namespace Domain.Interfaces;

public interface IApplicationRepository
{
    List<Application> GetAll();

    Application? GetById(int id);

    List<Application> GetByUserId(int userId);

    List<Application> GetByOfferId(int offerId);

    Application? GetByUserIdAndOfferId(int userId, int offerId);

    Application Create(Application application);

    void Update(Application application);

    void Delete(Application application);

    void SaveChanges();
}