using Application.DTOs.Company.Request;
using Application.DTOs.Company.Response;
using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _companyRepository;
    private readonly ICloudinaryService _cloudinaryService;

    public CompanyService(
        ICompanyRepository companyRepository,
        ICloudinaryService cloudinaryService)
    {
        _companyRepository = companyRepository;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<CompanyResponseDto> CreateAsync(
        CreateCompanyRequestDto request)
    {
        var company = new Company(
            request.BusinessName,
            request.Cuit,
            request.Sector,
            request.Description,
            request.Website,
            request.CreatedByRecruiterId);

        await _companyRepository.AddAsync(company);

        return MapToResponse(company);
    }

    public async Task<List<CompanyResponseDto>> GetAllAsync()
    {
        var companies =
            await _companyRepository.GetAllAsync();

        return companies
            .Select(MapToResponse)
            .ToList();
    }

    public async Task<CompanyResponseDto?> GetByIdAsync(
        int id)
    {
        var company =
            await _companyRepository.GetByIdAsync(id);

        if (company is null)
            return null;

        return MapToResponse(company);
    }

    public async Task UpdateAsync(
        int id,
        UpdateCompanyRequestDto request)
    {
        var company =
            await _companyRepository.GetByIdAsync(id);

        if (company is null)
            throw new Exception("Company not found");

        company.Update(
            request.BusinessName,
            request.Sector,
            request.Description,
            request.Website);

        await _companyRepository.UpdateAsync(company);
    }

    public async Task DeleteAsync(
        int id)
    {
        var company =
            await _companyRepository.GetByIdAsync(id);

        if (company is null)
            throw new Exception("Company not found");

        await _companyRepository.DeleteAsync(company);
    }

    public async Task UploadLogoAsync(
        int companyId,
        Stream stream,
        string fileName)
    {
        var company =
            await _companyRepository.GetByIdAsync(companyId);

        if (company is null)
            throw new Exception("Company not found");

        var uploadResult =
            await _cloudinaryService
                .UploadImageAsync(
                    stream,
                    fileName);

        company.SetLogo(
            uploadResult.Url,
            uploadResult.PublicId);

        await _companyRepository.UpdateAsync(company);
    }

    private static CompanyResponseDto MapToResponse(
        Company company)
    {
        return new CompanyResponseDto
        {
            Id = company.Id,
            BusinessName = company.BusinessName,
            ImageUrl = company.ImageUrl,
            Cuit = company.Cuit,
            Sector = company.Sector,
            Description = company.Description,
            Website = company.Website,
            Status = company.Status,
            CreatedAt = company.CreatedAt,
            CreatedByRecruiterId = company.CreatedByRecruiterId,
            ApprovedAt = company.ApprovedAt,
            ApprovedByAdminId = company.ApprovedByAdminId
        };
    }

    public async Task ApproveAsync(
    int companyId,
    int adminId)
    {
        var company =
            await _companyRepository
                .GetByIdAsync(companyId);

        if (company is null)
            throw new Exception("Company not found");

        company.Approve(adminId);

        await _companyRepository
            .UpdateAsync(company);
    }
}