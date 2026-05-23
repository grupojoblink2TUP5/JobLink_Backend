namespace Domain.Entities
{
    public class Notification
    {
        public int Id { get; private set; }
        public string? Title { get; private set; }
        public string? Message { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool IsRead { get; private set; }

        // Relación
        public int UserId { get; private set; }

        public Notification(string? message, string? title, int userId)
        {
            Message = message;
            Title = title;
            UserId = userId;
            CreatedAt = DateTime.Now;
            IsRead = false;
        }

        public void UpdateMessage(string? message)
        {
            Message = message;
        }

        public void MarkAsRead()
        {
            IsRead = true;
        }
    }
}