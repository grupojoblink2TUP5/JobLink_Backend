using Domain.Enums;

namespace Application.DTOs.User.Request;

public class UpdateUserRoleRequest
{
    public UserRole Role { get; set; }
}