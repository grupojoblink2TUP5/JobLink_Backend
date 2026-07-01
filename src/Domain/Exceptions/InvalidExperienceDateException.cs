namespace Domain.Exceptions;

public class InvalidExperienceDateException : DomainException
{
    public InvalidExperienceDateException()
        : base("End date must be greater than start date.")
    {
    }
}