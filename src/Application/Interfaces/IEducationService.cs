using Application.DTOs.Education.Request;
using Application.DTOs.Education.Response;

namespace Application.Interfaces;

public interface IEducationService
{
    List<EducationResponse> GetAllEducations();

    EducationResponse? GetEducationById(int id);

    List<EducationResponse> GetEducationsByUserId(int userId);

    Task<EducationResponse> CreateEducationAsync(CreateEducationRequest request);

    void UpdateEducation(int id, UpdateEducationRequest request);

    void DeleteEducation(int id);
}