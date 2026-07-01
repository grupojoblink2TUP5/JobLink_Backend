namespace Domain.Exceptions;

using Domain.Enums;

public class InvalidOfferTypeException : DomainException
{
    public InvalidOfferTypeException(OfferType offerType)
        : base($"Offer type '{offerType}' is invalid.")
    {
    }
}