using Application.DTOs.ApplicationHistory.Request;
using Application.DTOs.ApplicationHistory.Response;

namespace Application.Interfaces;

public interface IApplicationHistoryService
{
    List<ApplicationHistoryResponse> GetAllApplicationHistories();

    ApplicationHistoryResponse? GetApplicationHistoryById(int id);

    List<ApplicationHistoryResponse> GetApplicationHistoriesByApplicationId(int applicationId);

    ApplicationHistoryResponse CreateApplicationHistory(
        int applicationId,
        CreateApplicationRequest request
    );

    ApplicationHistoryResponse UpdateApplicationHistory(
        int id,
        UpdateApplicationRequest request
    );

    bool DeleteApplicationHistory(int id);
}