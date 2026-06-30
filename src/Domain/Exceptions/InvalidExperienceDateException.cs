namespace Domain.Exceptions;

public class InvalidExperienceDateException : Exception
{
    public InvalidExperienceDateException()
        : base("End date must be greater than start date.")
    {
    }
}