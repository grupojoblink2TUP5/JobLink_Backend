using System;

namespace Domain.Exceptions;

public class DuplicateApplicationException : Exception
{
    public DuplicateApplicationException(int userId, int offerId)
        : base($"El usuario {userId} ya se ha postulado a la oferta {offerId}.")
    {
    }

    public DuplicateApplicationException(string message)
        : base(message)
    {
    }

    public DuplicateApplicationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}