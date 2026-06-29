using Application.DTOs.User.Request;
using Application.DTOs.User.Response;
using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<UserResponse>> GetAllUsersAsync()
    {
        var users = await _repository.GetAllAsync();

        return users
            .Select(MapToResponse)
            .ToList();
    }

    public async Task<UserResponse> GetUserByIdAsync(int id)
    {
        var user = await _repository.GetByIdAsync(id);

        if (user is null)
            throw new NotFoundException(
                $"User not found for id = {id}");

        return MapToResponse(user);
    }

    public async Task<UserResponse> CreateUserAsync(
        CreateUserRequest request)
    {
        var user = new User(
            request.FirstName!,
            request.LastName!,
            request.Email!,
            request.Password!,
            request.Role!);

        await _repository.AddAsync(user);

        await _repository.SaveChangesAsync();

        return MapToResponse(user);
    }

    public async Task<UserResponse> UpdateUserAsync(
        int id,
        UpdateUserRequest request)
    {
        var user = await _repository.GetByIdAsync(id);

        if (user is null)
            throw new NotFoundException(
                $"User not found for id = {id}");

        user.Update(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);

        await _repository.UpdateAsync(user);

        await _repository.SaveChangesAsync();

        return MapToResponse(user);
    }

    public async Task<UserResponse> DeactivateAsync(int id)
    {
        var user = await _repository.GetByIdAsync(id);

        if (user is null)
            throw new NotFoundException(
                $"User not found for id = {id}");

        user.Deactivate();

        await _repository.UpdateAsync(user);

        await _repository.SaveChangesAsync();

        return MapToResponse(user);
    }

    public async Task<UserResponse> ActivateAsync(int id)
    {
        var user = await _repository.GetByIdAsync(id);

        if (user is null)
            throw new NotFoundException(
                $"User not found for id = {id}");

        user.Activate();

        await _repository.UpdateAsync(user);

        await _repository.SaveChangesAsync();

        return MapToResponse(user);
    }

    private static UserResponse MapToResponse(User user)
    {
        return new UserResponse(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email,
            user.RegistrationDate,
            user.Status,
            user.Role
        );
    }
}