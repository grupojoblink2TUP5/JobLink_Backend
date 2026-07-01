namespace Domain.Exceptions;

public class RecruiterDoesNotOwnCompanyException : DomainException
{
    public RecruiterDoesNotOwnCompanyException()
        : base("The recruiter is not the owner of this company.")
    {
    }
}