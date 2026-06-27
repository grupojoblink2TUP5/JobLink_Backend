using Application.DTOs.Application.Request;
using Application.DTOs.Application.Response;

namespace Application.Interfaces;

public interface IApplicationService
{
    List<ApplicationResponse> GetAllApplications();

    ApplicationResponse? GetApplicationById(int id);

    List<ApplicationResponse> GetApplicationsByUserId(int userId);

    List<ApplicationResponse> GetApplicationsByOfferId(int offerId);

    ApplicationResponse? GetApplicationByUserIdAndOfferId(int userId, int offerId);

    ApplicationResponse CreateApplication(int userId, CreateApplicationRequest request);

    ApplicationResponse UpdateApplication(int id, UpdateApplicationRequest request);

    bool DeleteApplication(int id);
}