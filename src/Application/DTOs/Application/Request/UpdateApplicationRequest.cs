using Domain.Enums;
namespace Application.DTOs.Application.Request;

public class UpdateApplicationRequest
{
    public ApplicationStatus CurrentStatus { get; set; }
}