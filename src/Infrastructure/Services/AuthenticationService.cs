using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.DTOs.Authentication.Request;
using Application.Interfaces;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;

public class AuthenticationService : ICustomAuthenticationService
{
    private readonly IUserRepository _repository;
    private readonly IConfiguration _configuration;

    public AuthenticationService(
        IUserRepository repository,
        IConfiguration configuration)
    {
        _repository = repository;
        _configuration = configuration;
    }

    public async Task<string> AuthenticateAsync(
        AuthenticationRequest request)
    {
        var user =
            await _repository.GetByEmailAsync(
                request.Email!);

        if (user is null || user.Password != request.Password)
        {
            throw new InvalidCredentialsException();
        }

        if (!user.Status)
        {
            throw new UserInactiveException(user.Email);
        }

        var securityKey =
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration["Authentication:SecretForKey"]!));

        var credentials =
            new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(
                ClaimTypes.NameIdentifier,
                user.Id.ToString()),

            new Claim(
                ClaimTypes.Role,
                user.Role.ToString()),

            new Claim(
                ClaimTypes.Email,
                user.Email)
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Authentication:Issuer"],
            audience: _configuration["Authentication:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }
}