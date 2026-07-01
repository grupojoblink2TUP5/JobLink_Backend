using System.Net.Mail;
using Application.DTOs.User.Request;
using Application.DTOs.User.Response;
using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Enums;

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
        var users =
            await _repository.GetAllAsync();

        return users
            .Select(MapToResponse)
            .ToList();
    }

    public async Task<UserResponse> GetUserByIdAsync(int id)
    {
        var user =
            await GetUserOrThrowAsync(id);

        return MapToResponse(user);
    }

    public async Task<UserResponse> CreateUserAsync(
        CreateUserRequest request)
    {
        ValidateUser(request);

        var existingUser =
            await _repository.GetByEmailAsync(
                request.Email!);

        if (existingUser is not null)
        {
            throw new UserAlreadyExistsException(
                request.Email!);
        }

        var user = new User(
        request.FirstName!,
        request.LastName!,
        request.Email!,
        request.Password!,
        UserRole.Candidate);

        await _repository.AddAsync(user);

        await _repository.SaveChangesAsync();

        return MapToResponse(user);
    }

    public async Task<UserResponse> UpdateUserAsync(
        int id,
        UpdateUserRequest request)
    {
        var user =
            await GetUserOrThrowAsync(id);

        ValidateUser(request);

        var existingUser =
            await _repository.GetByEmailAsync(
                request.Email!);

        if (existingUser is not null &&
            existingUser.Id != user.Id)
        {
            throw new UserAlreadyExistsException(
                request.Email!);
        }

        user.Update(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);

        await _repository.UpdateAsync(user);

        await _repository.SaveChangesAsync();

        return MapToResponse(user);
    }

    public async Task<UserResponse> ActivateAsync(int id)
    {
        var user =
            await GetUserOrThrowAsync(id);

        user.Activate();

        await _repository.UpdateAsync(user);

        await _repository.SaveChangesAsync();

        return MapToResponse(user);
    }

    public async Task<UserResponse> DeactivateAsync(int id)
    {
        var user =
            await GetUserOrThrowAsync(id);

        if (user.Role == UserRole.Admin)
        {
            throw new AdminCannotBeDeactivatedException();
        }

        user.Deactivate();

        await _repository.UpdateAsync(user);

        await _repository.SaveChangesAsync();

        return MapToResponse(user);
    }

    private async Task<User> GetUserOrThrowAsync(int id)
    {
        var user =
            await _repository.GetByIdAsync(id);

        if (user is null)
        {
            throw new NotFoundException(
                nameof(User),
                id);
        }

        return user;
    }

    private static void ValidateUser(
        CreateUserRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.FirstName))
            throw new FieldRequiredException(
                nameof(request.FirstName));

        if (string.IsNullOrWhiteSpace(request.LastName))
            throw new FieldRequiredException(
                nameof(request.LastName));

        if (string.IsNullOrWhiteSpace(request.Email))
            throw new FieldRequiredException(
                nameof(request.Email));

        if (string.IsNullOrWhiteSpace(request.Password))
            throw new FieldRequiredException(
                nameof(request.Password));

        try
        {
            _ = new MailAddress(request.Email);
        }
        catch
        {
            throw new InvalidEmailException(
                request.Email);
        }
    }

    private static void ValidateUser(
        UpdateUserRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.FirstName))
            throw new FieldRequiredException(
                nameof(request.FirstName));

        if (string.IsNullOrWhiteSpace(request.LastName))
            throw new FieldRequiredException(
                nameof(request.LastName));

        if (string.IsNullOrWhiteSpace(request.Email))
            throw new FieldRequiredException(
                nameof(request.Email));

        if (string.IsNullOrWhiteSpace(request.Password))
            throw new FieldRequiredException(
                nameof(request.Password));

        try
        {
            _ = new MailAddress(request.Email);
        }
        catch
        {
            throw new InvalidEmailException(
                request.Email);
        }
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
            user.Role);
    }

    public async Task<UserResponse> UpdateRoleAsync(
    int id,
    UpdateUserRoleRequest request)
    {
        var user =
            await GetUserOrThrowAsync(id);

        user.ChangeRole(request.Role);
        if (!Enum.IsDefined(typeof(UserRole), request.Role))
        {
            throw new InvalidUserRoleException(request.Role);
        }

        if (user.Role == UserRole.Admin &&
            request.Role != UserRole.Admin)
        {
            var adminCount =
                await _repository.CountAdminsAsync();

            if (adminCount == 1)
            {
                throw new Exception("Cannot remove the last admin user.");
            }
        }

        await _repository.UpdateAsync(user);

        await _repository.SaveChangesAsync();

        return MapToResponse(user);
    }
}