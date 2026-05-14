using Application.DTOs.Authentication.Request;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/authentication")]
public class AuthenticationController : ControllerBase
{
    private readonly ICustomAuthenticationService _service;

    public AuthenticationController(
        ICustomAuthenticationService service
    )
    {
        _service = service;
    }

    [HttpPost]
    public IActionResult Authenticate(
        [FromBody] AuthenticationRequest request
    )
    {
        var token = _service.Authenticate(request);

        return Ok(token);
    }
}