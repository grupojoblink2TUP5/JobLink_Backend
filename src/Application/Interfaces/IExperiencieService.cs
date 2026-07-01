using Application.DTOs.Experience.Request;
using Application.DTOs.Experience.Response;

namespace Application.Interfaces;

public interface IExperienceService
{
    List<ExperienceResponse> GetAllExperiences();

    ExperienceResponse? GetExperienceById(int id);

    List<ExperienceResponse> GetExperiencesByUserId(int userId);

    Task<ExperienceResponse> CreateExperienceAsync(CreateExperienceRequest request);

    void UpdateExperience(int id, UpdateExperienceRequest request);

    void DeleteExperience(int id);
}