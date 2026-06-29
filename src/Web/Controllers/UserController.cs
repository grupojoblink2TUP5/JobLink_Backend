using Application.DTOs.User.Request;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(
        IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users =
            await _userService.GetAllUsersAsync();

        return Ok(users);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user =
            await _userService.GetUserByIdAsync(id);

        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateUserRequest request)
    {
        var user =
            await _userService.CreateUserAsync(request);

        return CreatedAtAction(
            nameof(GetById),
            new { id = user.Id },
            user);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateUserRequest request)
    {
        var user =
            await _userService.UpdateUserAsync(
                id,
                request);

        return Ok(user);
    }

    [HttpPatch("{id:int}/activate")]
    public async Task<IActionResult> Activate(int id)
    {
        var user =
            await _userService.ActivateAsync(id);

        return Ok(user);
    }

    [HttpPatch("{id:int}/deactivate")]
    public async Task<IActionResult> Deactivate(int id)
    {
        var user =
            await _userService.DeactivateAsync(id);

        return Ok(user);
    }
}