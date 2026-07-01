namespace Domain.Exceptions;

public class UserIsNotAdminException : DomainException
{
    public UserIsNotAdminException(string email)
        : base($"The user '{email}' is not an admin.")
    {
    }
}