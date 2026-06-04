using Application.DTOs.User.Response;

namespace Application.DTOs.Candidate.Response;

public class CandidateResponse
{
    public int Id { get; init; }
    public int UserId { get; init; }
    public UserResponse User { get; init; } = null!;
    public CandidateResponse(int id, int userId, UserResponse user)
    {
        Id = id;
        UserId = userId;
        User = user;
    }
}