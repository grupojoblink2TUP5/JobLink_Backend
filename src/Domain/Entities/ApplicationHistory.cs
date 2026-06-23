namespace Domain.Entities
{
    public class ApplicationHistory
    {
        public int Id { get; private set; }
        public string Status { get; private set; }
        public string? Description { get; private set; }
        public DateTime ChangedAt { get; private set; }
        public bool VisibleToCandidate { get; private set; }

        // Relaciones
        public int ApplicationId { get; private set; }
        public int ChangedByRecruiterId { get; private set; }

        public ApplicationHistory(
            int applicationId,
            int changedByRecruiterId,
            string status,
            string? description,
            bool visibleToCandidate = true
        )
        {
            ApplicationId = applicationId;
            ChangedByRecruiterId = changedByRecruiterId;
            Status = status;
            Description = description;
            VisibleToCandidate = visibleToCandidate;
            ChangedAt = DateTime.UtcNow;
        }

        public void Update(string status, string? description, bool visibleToCandidate)
        {
            Status = status;
            Description = description;
            VisibleToCandidate = visibleToCandidate;
            ChangedAt = DateTime.UtcNow;
        }
    }
}