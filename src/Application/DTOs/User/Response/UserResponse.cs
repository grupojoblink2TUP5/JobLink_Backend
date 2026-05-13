using Domain.Enums;

namespace Application.DTOs.User.Response;

public class UserResponse
{
    public int Id { get; init; }

    public string? FirstName { get; init; }

    public string? LastName { get; init; }

    public string? Email { get; init; }

    public DateTime RegistrationDate { get; init; }

    public bool Status { get; init; }

    public UserRole Role { get; init; }

    public UserResponse(
        int id,
        string? firstName,
        string? lastName,
        string? email,
        DateTime registrationDate,
        bool status,
        UserRole role
    )
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        RegistrationDate = registrationDate;
        Status = status;
        Role = role;
    }
}