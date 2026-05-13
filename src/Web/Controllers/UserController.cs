using Application.DTOs.User.Request;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_userService.GetAllUsers());
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var user = _userService.GetUserById(id);

        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpPost]
    public IActionResult Create(CreateUserRequest request)
    {
        var user = _userService.CreateUser(request);

        return CreatedAtAction(
            nameof(GetById),
            new { id = user.Id },
            user
        );
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, UpdateUserRequest request)
    {
        var user = _userService.UpdateUser(id, request);

        return Ok(user);
    }

    [HttpPatch("{id:int}/activate")]
    public IActionResult Activate(int id)
    {
        try
        {
            var user = _userService.AddUser(id);

            return Ok(user);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPatch("{id:int}/deactivate")]
    public IActionResult Deactivate(int id)
    {
        try
        {
            var user = _userService.RemoveUser(id);

            return Ok(user);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}