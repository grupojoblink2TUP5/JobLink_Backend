namespace Domain.Entities
{
    public class Candidate
    {
        public int Id { get; private set; }
        public int UserId { get; private set; }
        public User User { get; private set; } = null!;
        
        private Candidate() { } // EF Core  

        public Candidate(User user)
        {
            User = user;
            UserId = user.Id;
        }

        public void Update(User user)
        {
            User = user;
            UserId = user.Id;
        }
    }
}