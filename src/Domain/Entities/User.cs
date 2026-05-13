using Domain.Enums;
namespace Domain.Entities;

public class User
{
    public int Id { get; private set; }

    public string FirstName { get; private set; } = null!;

    public string LastName { get; private set; } = null!;

    public string Email { get; private set; } = null!;

    public string Password { get; private set; } = null!;

    public DateTime RegistrationDate { get; private set; }

    public bool Status { get; private set; }

    public UserRole Role { get; private set; }

    private User() { }

    public User(
        string firstName,
        string lastName,
        string email,
        string password,
        UserRole role
    )
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        Role = role;
        RegistrationDate = DateTime.UtcNow;
        Status = true;
    }

    public void Update(
        string? firstName,
        string? lastName,
        string? email,
        string? password
    )
    {
        if (!string.IsNullOrEmpty(firstName))
            FirstName = firstName;

        if (!string.IsNullOrEmpty(lastName))
            LastName = lastName;

        if (!string.IsNullOrEmpty(email))
            Email = email;

        if (!string.IsNullOrEmpty(password))
            Password = password;
    }

    public void Activate()
    {
        Status = true;
    }

    public void Deactivate()
    {
        Status = false;
    }
}