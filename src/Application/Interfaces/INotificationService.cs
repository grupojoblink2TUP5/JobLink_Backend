using Application.DTOs.Notification.Request;
using Application.DTOs.Notification.Response;

namespace Application.Interfaces;

public interface INotificationService
{
    List<NotificationResponse> GetAllNotifications();

    NotificationResponse? GetNotificationById(int id);

    List<NotificationResponse> GetNotificationsByUserId(int userId);

    NotificationResponse CreateNotification(CreateNotificationRequest request);

    NotificationResponse UpdateNotification(int id, UpdateNotificationRequest request);

    bool DeleteNotification(int id);
}