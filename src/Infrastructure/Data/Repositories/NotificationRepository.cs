using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationContext _context;

        public NotificationRepository(ApplicationContext context)
        {
            _context = context;
        }

        public List<Notification> GetAll()
        {
            return _context.Notifications.ToList();
        }

        public Notification? GetById(int id)
        {
            return _context.Notifications.FirstOrDefault(n => n.Id == id);
        }

        public List<Notification> GetByUserId(int userId)
        {
            return _context.Notifications.Where(n => n.UserId == userId).ToList();
        }

        public Notification Create(Notification notification)
        {
            _context.Notifications.Add(notification);

            return notification;
        }

        public void Update(Notification notification)
        {
            _context.Notifications.Update(notification);
        }

        public void Delete(Notification notification)
        {
            _context.Notifications.Remove(notification);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}