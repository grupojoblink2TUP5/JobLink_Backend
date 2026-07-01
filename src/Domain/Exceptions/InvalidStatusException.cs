using System;

namespace Domain.Exceptions;

public class InvalidStatusException : Exception
{
    public InvalidStatusException()
        : base("The provided status is not valid.")
    {
    }

    public InvalidStatusException(string message)
        : base(message)
    {
    }

    public InvalidStatusException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}