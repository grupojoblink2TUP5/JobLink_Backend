using Domain.Entities;

namespace Domain.Interfaces;

public interface INotificationRepository
{
    List<Notification> GetAll();

    Notification? GetById(int id);

    List<Notification> GetByUserId(int userId);

    Notification Create(Notification notification);

    void Update(Notification notification);

    void Delete(Notification notification);

    void SaveChanges();
}