namespace Domain.Exceptions;

public class InvalidCuitException : DomainException
{
    public InvalidCuitException(string cuit)
        : base($"CUIT '{cuit}' is invalid.")
    {
    }
}