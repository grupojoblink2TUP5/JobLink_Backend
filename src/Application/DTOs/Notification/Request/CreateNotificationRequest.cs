namespace Application.DTOs.Notification.Request;

public class CreateNotificationRequest
{
    public string? Title { get; set; }
    public string? Message { get; set; }
    public int UserId { get; set; }
}