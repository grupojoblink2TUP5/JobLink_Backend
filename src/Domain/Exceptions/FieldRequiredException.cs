namespace Domain.Exceptions;

public class FieldRequiredException : DomainException
{
    public FieldRequiredException(string fieldName)
        : base($"Field '{fieldName}' is required.")
    {
    }
}