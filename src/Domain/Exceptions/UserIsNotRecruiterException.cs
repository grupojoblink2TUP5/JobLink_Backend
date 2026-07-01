namespace Domain.Exceptions;

public class UserIsNotRecruiterException : DomainException
{
    public UserIsNotRecruiterException(string email)
        : base($"The user '{email}' is not a recruiter.")
    {
    }
}