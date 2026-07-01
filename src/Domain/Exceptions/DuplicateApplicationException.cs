using System;

namespace Domain.Exceptions;

public class DuplicateApplicationException : DomainException
{
    public DuplicateApplicationException(int userId, int offerId)
        : base($"El usuario {userId} ya se ha postulado a la oferta {offerId}.")
    {
    }
}