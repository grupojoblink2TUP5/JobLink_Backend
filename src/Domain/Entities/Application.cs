using Domain.Enums;
namespace Domain.Entities
{
    public class Application
    {
        public int Id { get; private set; }
        public ApplicationStatus CurrentStatus { get; private set; }
        public DateTime AppliedAt { get; private set; }

        // Relaciones
        public int UserId { get; private set; }
        public int OfferId { get; private set; }

        public Application(int userId, int offerId)
        {
            UserId = userId;
            OfferId = offerId;
            AppliedAt = DateTime.Now;
            CurrentStatus = ApplicationStatus.Applied;
        }

        public void UpdateStatus(ApplicationStatus newStatus)
        {
            CurrentStatus = newStatus;
        }
    }
}