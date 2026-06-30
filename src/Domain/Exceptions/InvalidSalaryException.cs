namespace Domain.Exceptions;

public class InvalidSalaryException : DomainException
{
    public InvalidSalaryException(decimal salary)
        : base($"Salary '{salary}' is invalid. Salary must be greater than zero.")
    {
    }
}