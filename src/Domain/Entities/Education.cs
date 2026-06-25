namespace Domain.Entities
{
    public class Education
    {
        public int Id { get; private set; }
        public string? InstitutionName { get; private set; }
        public string? Degree { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }

        public int UserId { get; private set; }

        private Education() { }

        public Education(
            string? institutionName,
            string? degree,
            DateTime startDate,
            DateTime? endDate,
            int userId
        )
        {
            InstitutionName = institutionName;
            Degree = degree;
            StartDate = startDate;
            EndDate = endDate;
            UserId = userId;
        }

        public void UpdateEducation(
            string? institutionName,
            string? degree,
            DateTime startDate,
            DateTime? endDate
        )
        {
            InstitutionName = institutionName;
            Degree = degree;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}