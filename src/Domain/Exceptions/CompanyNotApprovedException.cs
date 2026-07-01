namespace Domain.Exceptions;

public class CompanyNotApprovedException : DomainException
{
    public CompanyNotApprovedException(string companyName)
        : base($"The company '{companyName}' has not been approved yet.")
    {
    }
}