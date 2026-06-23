

namespace Domain.Entities
{
    public class Experience
    {
        public int Id { get; private set; }
        public string? CompanyName { get; private set; }
        public string? Position { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public string? Description { get; private set; }
        
        public int UserId { get; private set; }

        private Experience() { }

        public Experience(
            string? companyName,
            string? position,
            DateTime startDate,
            DateTime? endDate,
            string? description,
            int userId
        )
        {
            CompanyName = companyName;
            Position = position;
            StartDate = startDate;
            EndDate = endDate;
            Description = description;
            UserId = userId;
        }

        public void UpdateExperience(
            string? companyName,
            string? position,
            DateTime startDate,
            DateTime? endDate,
            string? description
        )
        {
            CompanyName = companyName;
            Position = position;
            StartDate = startDate;
            EndDate = endDate;
            Description = description;
        }
    }
}