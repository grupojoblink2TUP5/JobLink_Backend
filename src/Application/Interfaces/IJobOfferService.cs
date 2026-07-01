using Application.DTOs.JobOffer.Request;
using Application.DTOs.JobOffer.Response;

namespace Application.Interfaces;

public interface IJobOfferService
{
    Task<JobOfferResponseDto> CreateAsync(
        CreateJobOfferRequestDto request);

    Task<List<JobOfferResponseDto>> GetAllAsync();

    Task<JobOfferResponseDto> GetByIdAsync(int id);

    Task<List<JobOfferResponseDto>> GetByCompanyIdAsync(int companyId);

    Task<List<JobOfferResponseDto>> GetOpenOffersAsync();

    Task UpdateAsync(
        int id,
        UpdateJobOfferRequestDto request);

    Task DeleteAsync(int id);

    Task CloseAsync(int id);

    Task PauseAsync(int id);

    Task ReopenAsync(int id);
}