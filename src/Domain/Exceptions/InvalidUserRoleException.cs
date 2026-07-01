namespace Domain.Exceptions;

using Domain.Enums;

public class InvalidUserRoleException : DomainException
{
    public InvalidUserRoleException(UserRole role)
        : base($"'{role}' is not a valid user role.")
    {
    }
}