namespace Domain.Exceptions;

public class InvalidClosingDateException : DomainException
{
    public InvalidClosingDateException(DateTime closingDate)
        : base($"Closing date '{closingDate:dd/MM/yyyy}' must be later than today.")
    {
    }
}