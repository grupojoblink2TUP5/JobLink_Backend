using Application.DTOs.Notification.Request;
using Application.DTOs.Notification.Response;  
using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _repository;

    public NotificationService(INotificationRepository repository)
    {
        _repository = repository;
    }

    public List<NotificationResponse> GetAllNotifications()
    {
        return _repository
            .GetAll()
            .Select(notification => new NotificationResponse(
                notification.Id,
                notification.Title,
                notification.Message,
                notification.CreatedAt,
                notification.IsRead,
                notification.UserId
            ))
            .ToList();
    }

    public NotificationResponse? GetNotificationById(int id)
    {
        var notification = _repository.GetById(id);

        if (notification == null)
        {
            return null;
        }

        return new NotificationResponse(
            notification.Id,
            notification.Title,
            notification.Message,
            notification.CreatedAt,
            notification.IsRead,
            notification.UserId
        );
    }

    public List<NotificationResponse> GetNotificationsByUserId(int userId)
    {
        return _repository
            .GetByUserId(userId)
            .Select(notification => new NotificationResponse(
                notification.Id,
                notification.Title,
                notification.Message,
                notification.CreatedAt,
                notification.IsRead,
                notification.UserId
            ))
            .ToList();
    }

    public NotificationResponse CreateNotification(CreateNotificationRequest request)
    {
        var notification = new Notification(
            request.Message,
            request.Title,
            request.UserId
        );

        _repository.Create(notification);
        _repository.SaveChanges();

        return new NotificationResponse(
            notification.Id,
            notification.Title,
            notification.Message,
            notification.CreatedAt,
            notification.IsRead,
            notification.UserId
        );
    }

    public NotificationResponse UpdateNotification(int id, UpdateNotificationRequest request)
    {
        var notification = _repository.GetById(id);

        if (notification == null)
        {
            throw new NotFoundException($"Notification not found for id = {id}");
        }

        notification.UpdateMessage(request.Message);
        notification.MarkAsRead(); // Assuming you have a method to mark as read

        _repository.Update(notification);
        _repository.SaveChanges();

        return new NotificationResponse(
            notification.Id,
            notification.Title,
            notification.Message,
            notification.CreatedAt,
            notification.IsRead,
            notification.UserId
        );
    }

    public bool DeleteNotification(int id)
    {
        var notification = _repository.GetById(id);

        if (notification == null)
        {
            return false;
        }

        _repository.Delete(notification);
        _repository.SaveChanges();

        return true;
    }
}