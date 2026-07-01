using Application.DTOs.Company.Request;
using Application.DTOs.Company.Response;

namespace Application.Interfaces;

public interface ICompanyService
{
    Task<CompanyResponseDto> CreateAsync(
        CreateCompanyRequestDto request);

    Task<List<CompanyResponseDto>> GetAllAsync();

    Task<CompanyResponseDto> GetByIdAsync(int id);

    Task UpdateAsync(
        int id,
        UpdateCompanyRequestDto request);

    Task DeleteAsync(
        int id);

    Task UploadLogoAsync(
        int companyId,
        Stream stream,
        string fileName);

    Task ApproveAsync(
        int companyId,
        int adminId);
}