namespace Application.DTOs.Notification.Request;

public class UpdateNotificationRequest
{
    public string? Message { get; set; }
    public bool IsRead { get; set; }
}