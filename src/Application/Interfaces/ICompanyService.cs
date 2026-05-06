using Application.DTOs.Company.Request;
using Application.DTOs.Company.Response;

namespace Application.Interfaces;

public interface ICompanyService
{
    List<CompanyResponse> GetAllCompanies();

    CompanyResponse? GetCompanyById(int id);

    CompanyResponse CreateCompany(CreateCompanyRequest request);

    CompanyResponse UpdateCompany(int id, UpdateCompanyRequest request);

    bool DeleteCompany(int id);

    CompanyResponse ApproveCompany(int id);

    CompanyResponse RejectCompany(int id);
}