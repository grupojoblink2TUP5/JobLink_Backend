using Application.DTOs.User.Request;
using Application.DTOs.User.Response;

namespace Application.Interfaces;

public interface IUserService
{
    Task<List<UserResponse>> GetAllUsersAsync();

    Task<UserResponse> GetUserByIdAsync(int id);

    Task<UserResponse> CreateUserAsync(CreateUserRequest request);

    Task<UserResponse> UpdateUserAsync(
        int id,
        UpdateUserRequest request);

    Task<UserResponse> ActivateAsync(int id);

    Task<UserResponse> DeactivateAsync(int id);
}