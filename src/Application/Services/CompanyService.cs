using Application.DTOs.Company.Request;
using Application.DTOs.Company.Response;
using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _repository;

        public CompanyService(ICompanyRepository repository)
        {
            _repository = repository;
        }

        public List<CompanyResponse> GetAllCompanies()
        {
            return _repository
                .GetAll()
                .Select(company => new CompanyResponse(
                    company.Id,
                    company.BusinessName,
                    company.Cuit,
                    company.Industry,
                    company.Description,
                    company.Website,
                    company.Location,
                    company.Approved
                ))
                .ToList();
        }

        public CompanyResponse GetCompanyById(int id)
        {
            var company = _repository.GetById(id);

            if (company == null)
            {
                throw new NotFoundException($"Company not found for id = {id}");
            }

            return MapToResponse(company);
        }

        public CompanyResponse CreateCompany(CreateCompanyRequest request)
        {
            var company = new Company(
                request.BusinessName!,
                request.Cuit!,
                request.Industry!,
                request.Description!,
                request.Website!,
                request.Location!
            );

            _repository.Create(company);

            _repository.SaveChanges();

            return MapToResponse(company);
        }

        public CompanyResponse UpdateCompany(int id, UpdateCompanyRequest request)
        {
            var company = _repository.GetById(id);

            if (company == null)
            {
                throw new Exception("Company not found");
            }

            company.Update(
                request.Industry,
                request.Description,
                request.Website,
                request.Location
            );

            _repository.Update(company);

            _repository.SaveChanges();

            return MapToResponse(company);
        }

        public bool DeleteCompany(int id)
        {
            var company = _repository.GetById(id);

            if (company == null)
            {
                return false;
            }

            _repository.Delete(company);

            _repository.SaveChanges();

            return true;
        }

        public CompanyResponse ApproveCompany(int id)
        {
            var company = _repository.GetById(id);

            if (company == null)
            {
                throw new Exception("Company not found");
            }

            company.Approve();

            _repository.Update(company);

            _repository.SaveChanges();

            return MapToResponse(company);
        }

        public CompanyResponse RejectCompany(int id)
        {
            var company = _repository.GetById(id);

            if (company == null)
            {
                throw new Exception("Company not found");
            }

            company.Reject();

            _repository.Update(company);

            _repository.SaveChanges();

            return MapToResponse(company);
        }

        private static CompanyResponse MapToResponse(Company company)
        {
            return new CompanyResponse(
                company.Id,
                company.BusinessName,
                company.Cuit,
                company.Industry,
                company.Description,
                company.Website,
                company.Location,
                company.Approved
            );
        }
    }
}