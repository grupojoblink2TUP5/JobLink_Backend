namespace Domain.Exceptions;

public class JobOfferNotFoundException : DomainException
{
    public JobOfferNotFoundException(int id)
        : base($"Job offer with id {id} was not found.")
    {
    }
}