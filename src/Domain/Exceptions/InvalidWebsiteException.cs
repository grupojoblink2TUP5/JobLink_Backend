namespace Domain.Exceptions;

public class InvalidWebsiteException : DomainException
{
    public InvalidWebsiteException(string website)
        : base($"'{website}' is not a valid website.")
    {
    }
}