namespace Application.DTOs.Notification.Response;

public class NotificationResponse
{
    public int Id { get; init; }
    public string? Title { get; init; }
    public string? Message { get; init; }
    public DateTime CreatedAt { get; init; }
    public bool IsRead { get; init; }

    public int UserId { get; init; }

    public NotificationResponse(
        int id,
        string? title,
        string? message,
        DateTime createdAt,
        bool isRead,
        int userId
    )
    {
        Id = id;
        Title = title;
        Message = message;
        CreatedAt = createdAt;
        IsRead = isRead;
        UserId = userId;
    }
}