using Application.DTOs.ApplicationHistory.Request;
using Application.DTOs.ApplicationHistory.Response;

namespace Application.Interfaces;

public interface IApplicationHistoryService
{
    Task<List<ApplicationHistoryResponse>> GetAllApplicationHistoriesAsync();

    Task<ApplicationHistoryResponse> GetApplicationHistoryByIdAsync(int id);

    Task<List<ApplicationHistoryResponse>> GetApplicationHistoriesByApplicationIdAsync(int applicationId);

    Task<ApplicationHistoryResponse> CreateApplicationHistoryAsync(
        int applicationId,
        CreateApplicationHistoryRequest request
    );

    Task<ApplicationHistoryResponse> UpdateApplicationHistoryAsync(
        int id,
        UpdateApplicationHistoryRequest request
    );

    Task<bool> DeleteApplicationHistoryAsync(int id);
}