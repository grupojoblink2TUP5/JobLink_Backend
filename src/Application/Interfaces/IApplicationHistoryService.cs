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
        CreateApplicationHistoryRequest request
    );

    ApplicationHistoryResponse UpdateApplicationHistory(
        int id,
        UpdateApplicationHistoryRequest request
    );

    bool DeleteApplicationHistory(int id);
}