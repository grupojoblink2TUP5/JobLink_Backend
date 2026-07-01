using System;

namespace Domain.Exceptions;

public class ForbiddenException : Exception
{
    public ForbiddenException()
        : base("The operation is not allowed.")
    {
    }

    public ForbiddenException(string message)
        : base(message)
    {
    }

    public ForbiddenException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}