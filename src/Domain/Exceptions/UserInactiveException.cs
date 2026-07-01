namespace Domain.Exceptions;

public class UserInactiveException : DomainException
{
    public UserInactiveException(string email)
        : base($"The user '{email}' is inactive.")
    {
    }
}