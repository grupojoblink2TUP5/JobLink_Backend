using System;

namespace Domain.Exceptions;

public class InvalidStatusException : DomainException
{
    public InvalidStatusException()
        : base("The provided status is not valid.")
    {
    }
}