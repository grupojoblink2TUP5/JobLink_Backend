namespace Domain.Exceptions;

public class InvalidEducationDateException : Exception
{
    public InvalidEducationDateException()
        : base("End date must be greater than start date.")
    {
    }
}