using Application.DTOs.Authentication.Request;

namespace Application.Interfaces;

public interface ICustomAuthenticationService
{
    string Authenticate(AuthenticationRequest request);
}