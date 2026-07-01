namespace Domain.Exceptions;

public class RecruiterNotFoundException : DomainException
{
    public RecruiterNotFoundException()
        : base("Recruiter not found.")
    {
    }
}