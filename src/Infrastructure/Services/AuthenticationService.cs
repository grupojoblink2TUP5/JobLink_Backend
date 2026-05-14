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
        IConfiguration configuration
    )
    {
        _repository = repository;
        _configuration = configuration;
    }

    public string Authenticate(AuthenticationRequest request)
    {
        var user = _repository.GetByEmail(request.Email!);

        if (user == null || user.Password != request.Password)
        {
            throw new InvalidCredentialsException();
        }

        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                _configuration["Authentication:SecretForKey"]!
            )
        );

        var credentials = new SigningCredentials(
            securityKey,
            SecurityAlgorithms.HmacSha256
        );

        var claims = new List<Claim>
        {
            new Claim("sub", user.Id.ToString()),
            new Claim("role", user.Role.ToString()),
            new Claim("email", user.Email)
        };

        var token = new JwtSecurityToken(
            _configuration["Authentication:Issuer"],
            _configuration["Authentication:Audience"],
            claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}