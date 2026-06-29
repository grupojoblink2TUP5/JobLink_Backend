using System;

namespace Domain.Exceptions;

public class DuplicateApplicationException : Exception
{
    public DuplicateApplicationException()
        : base("El usuario ya se ha postulado a esta oferta.")
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
