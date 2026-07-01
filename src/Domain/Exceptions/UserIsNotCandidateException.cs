namespace Domain.Exceptions;

public class UserIsNotCandidateException : DomainException
{
    public UserIsNotCandidateException(string email)
        : base($"User '{email}' is not a candidate.")
    {
    }
}