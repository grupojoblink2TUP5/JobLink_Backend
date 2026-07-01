namespace Domain.Exceptions;

public class AdminCannotBeDeactivatedException : DomainException
{
    public AdminCannotBeDeactivatedException()
        : base("Administrators cannot be deactivated.")
    {
    }
}