namespace Domain.Exceptions;

public class InvalidEducationDateException : DomainException
{
    public InvalidEducationDateException()
        : base("End date must be greater than start date.")
    {
    }
}