namespace Domain.Entities
{
    public class Experience
    {
        public int Id { get; private set; }
        public string CompanyName { get; private set; }
        public string Position { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public string Description { get; private set; }

        public int CandidateId { get; private set; }

        public Experience(
            string companyName,
            string position,
            DateTime startDate,
            DateTime? endDate,
            string description,
            int candidateId
        )
        {
            CompanyName = companyName;
            Position = position;
            StartDate = startDate;
            EndDate = endDate;
            Description = description;
            CandidateId = candidateId;
        }

        public void Update(
            string? companyName,
            string? position,
            DateTime startDate,
            DateTime? endDate,
            string? description
        )
        {
            if (companyName != null) CompanyName = companyName;
            if (position != null) Position = position;
            StartDate = startDate;
            EndDate = endDate;
            if (description != null) Description = description;
        }
    }
}