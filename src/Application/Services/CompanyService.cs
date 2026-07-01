using Application.DTOs.Company.Request;
using Application.DTOs.Company.Response;
using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Enums;

namespace Application.Services;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _companyRepository;
    private readonly ICloudinaryService _cloudinaryService;

    private readonly IUserRepository _userRepository;

    public CompanyService(
        ICompanyRepository companyRepository,
        IUserRepository userRepository,
        ICloudinaryService cloudinaryService)
    {
        _userRepository = userRepository;
        _companyRepository = companyRepository;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<CompanyResponseDto> CreateAsync(
        CreateCompanyRequestDto request)
    {
        ValidateCompany(request);

        var recruiter =
        await _userRepository.GetByIdAsync(
        request.CreatedByRecruiterId);

        if (recruiter is null)
        {
            throw new NotFoundException(
                nameof(User),
                request.CreatedByRecruiterId);
        }

        if (recruiter.Role != UserRole.Recruiter)
        {
            throw new UserIsNotRecruiterException(
                recruiter.Email);
        }

        if (!recruiter.Status)
        {
            throw new UserInactiveException(
                recruiter.Email);
        }

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

    public async Task<CompanyResponseDto> GetByIdAsync(
        int id)
    {
        var company =
            await GetCompanyOrThrowAsync(id);

        return MapToResponse(company);
    }

    public async Task UpdateAsync(
        int id,
        UpdateCompanyRequestDto request)
    {
        var company =
            await GetCompanyOrThrowAsync(id);

        ValidateCompany(request);

        company.Update(
            request.BusinessName,
            request.Sector,
            request.Description,
            request.Website);

        await _companyRepository.UpdateAsync(company);
    }

    public async Task DeleteAsync(int id)
    {
        var company =
            await GetCompanyOrThrowAsync(id);

        await _companyRepository.DeleteAsync(company);
    }

    public async Task UploadLogoAsync(
        int companyId,
        Stream stream,
        string fileName)
    {
        var company =
            await GetCompanyOrThrowAsync(companyId);

        var uploadResult =
            await _cloudinaryService.UploadImageAsync(
                stream,
                fileName);

        company.SetLogo(
            uploadResult.Url,
            uploadResult.PublicId);

        await _companyRepository.UpdateAsync(company);
    }

    public async Task ApproveAsync(
        int companyId,
        int adminId)
    {
        var company =
            await GetCompanyOrThrowAsync(companyId);

        var admin =
        await _userRepository.GetByIdAsync(adminId);

        if (admin is null)
        {
            throw new NotFoundException(
                nameof(User),
                adminId);
        }

        if (!admin.Status)
        {
            throw new UserInactiveException(
                admin.Email);
        }

        if (admin.Role != UserRole.Admin)
        {
            throw new UserIsNotAdminException(admin.Email);
        }

        company.Approve(adminId);

        await _companyRepository.UpdateAsync(company);
    }

    private async Task<Company> GetCompanyOrThrowAsync(
        int id)
    {
        var company =
            await _companyRepository.GetByIdAsync(id);

        if (company is null)
        {
            throw new NotFoundException(
                nameof(Company),
                id);
        }

        return company;
    }

    private static void ValidateCompany(
        CreateCompanyRequestDto request)
    {
        if (string.IsNullOrWhiteSpace(request.BusinessName))
            throw new FieldRequiredException(nameof(request.BusinessName));

        if (string.IsNullOrWhiteSpace(request.Cuit))
            throw new FieldRequiredException(nameof(request.Cuit));

        if (request.Cuit.Length != 11)
            throw new InvalidCuitException(request.Cuit);

        if (string.IsNullOrWhiteSpace(request.Sector))
            throw new FieldRequiredException(nameof(request.Sector));

        if (string.IsNullOrWhiteSpace(request.Description))
            throw new FieldRequiredException(nameof(request.Description));

        if (string.IsNullOrWhiteSpace(request.Website))
            throw new FieldRequiredException(nameof(request.Website));

        if (!Uri.TryCreate(
                request.Website,
                UriKind.Absolute,
                out _))
        {
            throw new InvalidWebsiteException(
                request.Website);
        }
    }

    private static void ValidateCompany(
        UpdateCompanyRequestDto request)
    {
        if (string.IsNullOrWhiteSpace(request.BusinessName))
            throw new FieldRequiredException(nameof(request.BusinessName));

        if (string.IsNullOrWhiteSpace(request.Sector))
            throw new FieldRequiredException(nameof(request.Sector));

        if (string.IsNullOrWhiteSpace(request.Description))
            throw new FieldRequiredException(nameof(request.Description));

        if (string.IsNullOrWhiteSpace(request.Website))
            throw new FieldRequiredException(nameof(request.Website));

        if (!Uri.TryCreate(
                request.Website,
                UriKind.Absolute,
                out _))
        {
            throw new InvalidWebsiteException(
                request.Website);
        }
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
}