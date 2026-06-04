using Application.DTOs.Candidate.Request;
using Application.DTOs.Candidate.Response;

namespace Application.Interfaces;

public interface ICandidateService
{
    List<CandidateResponse> GetAllCandidates();

    CandidateResponse? GetCandidateById(int id);

    CandidateResponse? GetCandidateByUserId(int userId);

    CandidateResponse CreateCandidate(CreateCandidateRequest request);

    CandidateResponse UpdateCandidate(int id, UpdateCandidateRequest request);

    bool DeleteCandidate(int id);
}