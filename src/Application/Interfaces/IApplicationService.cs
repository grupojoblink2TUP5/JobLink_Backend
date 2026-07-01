using Application.DTOs.Application.Request;
using Application.DTOs.Application.Response;

namespace Application.Interfaces;

public interface IApplicationService
{
    Task<List<ApplicationResponse>> GetAllApplicationsAsync();

    Task<ApplicationResponse> GetApplicationByIdAsync(int id);

    Task<List<ApplicationResponse>> GetApplicationsByUserIdAsync(int userId);

    Task<List<ApplicationResponse>> GetApplicationsByOfferIdAsync(int offerId);

    Task<ApplicationResponse?> GetApplicationByUserIdAndOfferIdAsync(int userId, int offerId);

    Task<ApplicationResponse> CreateApplicationAsync(int userId, CreateApplicationRequest request);

    Task<bool> DeleteApplicationAsync(int id);
}