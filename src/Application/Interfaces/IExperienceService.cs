using Application.DTOs.Experience.Request;
using Application.DTOs.Experience.Response;

namespace Application.Interfaces;

public interface IExperienceService
{
    List<ExperienceResponse> GetAllExperiences();

    ExperienceResponse? GetExperienceById(int id);

    ExperienceResponse? GetExperienceByCandidateId(int candidateId);

    ExperienceResponse CreateExperience(CreateExperienceRequest request);

    ExperienceResponse UpdateExperience(int id, UpdateExperienceRequest request);

    bool DeleteExperience(int id);
}