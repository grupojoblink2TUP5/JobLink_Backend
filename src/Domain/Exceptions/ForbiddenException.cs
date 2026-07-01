using System;

namespace Domain.Exceptions;

public class ForbiddenException : DomainException
{
    public ForbiddenException()
        : base("The operation is not allowed.")
    {
    }
}