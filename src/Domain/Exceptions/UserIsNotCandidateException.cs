namespace Domain.Exceptions;

public class UserIsNotCandidateException : Exception
{
    public UserIsNotCandidateException(string email)
        : base($"User '{email}' is not a candidate.")
    {
    }
}