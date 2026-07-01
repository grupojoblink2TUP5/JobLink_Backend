using Application.DTOs.Authentication.Request;

namespace Application.Interfaces;

public interface ICustomAuthenticationService
{
    Task<string> AuthenticateAsync(
        AuthenticationRequest request);
}